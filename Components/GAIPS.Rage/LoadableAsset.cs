using System;
using System.IO;
using AssetManagerPackage;
using AssetPackage;
using SerializationUtilities;
using Utilities.Json;

namespace GAIPS.Rage
{
	public abstract class LoadableAsset<T> : BaseAsset where T : LoadableAsset<T>
	{
		protected static readonly JSONSerializer SERIALIZER = new JSONSerializer();

		[NonSerialized] private string m_assetFilepath = null;
		public string AssetFilePath => m_assetFilepath;

		public static T LoadFromFile(string filePath)
		{
			string error;
			T asset = LoadFromFile(filePath, out error);
			if (error != null)
				throw new Exception(error); //TODO better exception
			return asset;
		}


	    public static T LoadFromString(string text)
	    {
	        string error;
	        T asset = LoadFromString(text, out error);
	        if (error != null)
	            throw new Exception(error); //TODO better exception
	        return asset;
	    }


		public static T LoadFromFile(string filePath, out string errorOnLoad)
		{
			var storage = GetInterface<IDataStorage>();
			if (storage == null)
				throw new Exception($"No {nameof(IDataStorage)} defined in the AssetManager bridge.");

		    if (!storage.Exists(filePath))
				throw new FileNotFoundException();

			var data = storage.Load(filePath);
			T asset;
			try
			{
				asset = SERIALIZER.DeserializeFromJson<T>((JsonObject)JsonParser.Parse(data));
			}
			catch (Exception e)
			{
				errorOnLoad = e.Message + "\n" + e.StackTrace;
				return null;
			}
			asset.m_assetFilepath = filePath;
			errorOnLoad = asset.OnAssetLoaded();
			return asset;
		}


	    public static T LoadFromString(string _text, out string errorOnLoad)
	    {
	        var data = _text;
	        T asset;
	        try
	        {
	            asset = SERIALIZER.DeserializeFromJson<T>((JsonObject)JsonParser.Parse(data));
	        }
	        catch (Exception e)
	        {
	            errorOnLoad = e.Message + "\n" + e.StackTrace;
	            return null;
	        }
	       
	        errorOnLoad = asset.OnAssetLoaded();
	        return asset;
	    }


		/// <returns>Error message if any. Null if the asset was loaded without errors</returns>
		protected abstract string OnAssetLoaded();

		protected virtual void OnAssetPathChanged(string oldpath){}

		public void Save()
		{
			if(string.IsNullOrEmpty(m_assetFilepath))
				throw new Exception("No default file path defined for the asset. Please use SaveToFile(filepath).");
            
			SaveToFile(m_assetFilepath);
		}

		public void SaveToFile(string filepath)
		{
			var storage = GetInterface<IDataStorage>();
			if(storage == null)
				throw new Exception($"No {nameof(IDataStorage)} defined in the AssetManager bridge.");

			if (!string.Equals(m_assetFilepath, filepath))
			{
				var oldPath = m_assetFilepath;
				m_assetFilepath = filepath;
				if(!string.IsNullOrEmpty(oldPath))
					OnAssetPathChanged(oldPath);
			}

			var json = SERIALIZER.SerializeToJson(this);
			storage.Save(filepath, json.ToString(true));
		}

		protected string ToRelativePath(string absolutePath)
		{
			if (string.IsNullOrEmpty(absolutePath))
				return string.Empty;
			if (string.IsNullOrEmpty(m_assetFilepath))
				return absolutePath;

			return ToRelativePath(m_assetFilepath, absolutePath);
		}

		protected string ToAbsolutePath(string relativePath)
		{
			if (string.IsNullOrEmpty(relativePath))
				return string.Empty;
			if (string.IsNullOrEmpty(m_assetFilepath))
				return relativePath;

			return ToAbsolutePath(m_assetFilepath, relativePath);
		}

		public static string ToRelativePath(string basePath, string absolutePath)
		{
			if (!Path.IsPathRooted(absolutePath))
				return absolutePath;

			if (Path.HasExtension(basePath))
				basePath = PathUtilities.GetDirectoryName(basePath);

			return PathUtilities.RelativePath(absolutePath, basePath);
		}

		protected static string ToAbsolutePath(string basePath, string relativePath)
		{
            //todo: special case
            if (!basePath.Contains("/") && !relativePath.Contains("/"))
                return relativePath;

			if (Path.HasExtension(basePath))
				basePath = PathUtilities.GetDirectoryName(basePath);
                       
			return PathUtilities.CleanCombine(basePath, relativePath);
		}

		protected static I GetInterface<I>()
		{
            if (AssetManager.Instance.Bridge != null && AssetManager.Instance.Bridge is I)
			{
				return (I) (AssetManager.Instance.Bridge);
            }
            else if(AssetManager.Instance.Bridge == null)
            {
                AssetManager.Instance.Bridge = new BasicIOBridge();
                return (I)(AssetManager.Instance.Bridge);
            }
			return default(I);
		}
	}
}