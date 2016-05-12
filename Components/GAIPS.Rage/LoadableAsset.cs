using System;
using System.IO;
using AssetPackage;
using GAIPS.Serialization;

namespace GAIPS.Rage
{
	public abstract class LoadableAsset<T> : BaseAsset
		where T : LoadableAsset<T>
	{
		[NonSerialized]
		private string m_assetFilepath = null;
		public string AssetFilePath => m_assetFilepath;

		public static T LoadFromFile(string filename)
		{
			string error;
			T asset = LoadFromFile(filename, out error);
			if(error!=null)
				throw new Exception(error);	//TODO better exception
			return asset;
		}

		public static T LoadFromFile(string filename, out string errorOnLoad)
		{
			T asset;
			using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
			{
				var serializer = new JSONSerializer();
				asset = serializer.Deserialize<T>(f);
			}

			asset.m_assetFilepath = Path.GetFullPath(filename);
			errorOnLoad = asset.OnAssetLoaded();
			return asset;
		}

		/// <returns>Error message if any. Null if the asset was loaded without errors</returns>
		protected abstract string OnAssetLoaded();

		protected virtual void OnAssetPathChanged(string oldpath){}

		public void SaveToFile(string filepath)
		{
			filepath = Path.GetFullPath(filepath);
			if (!string.Equals(m_assetFilepath, filepath))
			{
				var old = m_assetFilepath;
				m_assetFilepath = filepath;
				OnAssetPathChanged(old);
			}

			using (var f = File.Open(filepath, FileMode.Create, FileAccess.Write))
			{
				var serializer = new JSONSerializer();
				serializer.Serialize(f,this);
			}
		}

		protected string ToRelativePath(string absolutePath)
		{
			if (m_assetFilepath == null)
				return absolutePath;
			return ToRelativePath(m_assetFilepath, absolutePath);
		}

		protected string ToAbsolutePath(string relativePath)
		{
			if (m_assetFilepath == null)
				return relativePath;
			return ToAbsolutePath(m_assetFilepath, relativePath);
		}

		protected static string ToRelativePath(string basePath,string absolutePath)
		{
			if (!Path.IsPathRooted(absolutePath))
				return absolutePath;

			var abs = new Uri(absolutePath);
			var origin = new Uri(basePath);
			return origin.MakeRelativeUri(abs).ToString();
		}

		protected static string ToAbsolutePath(string basePath, string relativePath)
		{
			var att = File.GetAttributes(basePath);
			
			if ((att & FileAttributes.Directory) != FileAttributes.Directory)
				basePath = Path.GetDirectoryName(basePath);

			return Path.GetFullPath(Path.Combine(basePath, relativePath));
		}
	}
}