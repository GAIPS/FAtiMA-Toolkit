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
		protected IStorageProvider CurrentStorageProvider { get; private set; }

		public static T LoadFromFile(IStorageProvider storageProvider, string filename)
		{
			string error;
			T asset = LoadFromFile(storageProvider, filename, out error);
			if(error!=null)
				throw new Exception(error);	//TODO better exception
			return asset;
		}

		public static T LoadFromFile(IStorageProvider storageProvider, string filename, out string errorOnLoad)
		{
			T asset;
			using (var f = storageProvider.RequestFile(filename, FileMode.Open, FileAccess.Read))
			{
				var serializer = new JSONSerializer();
				asset = serializer.Deserialize<T>(f);
			}

			asset.CurrentStorageProvider = storageProvider;
			asset.m_assetFilepath = storageProvider.GetFullPath(filename);
			errorOnLoad = asset.OnAssetLoaded();
			return asset;
		}

		/// <returns>Error message if any. Null if the asset was loaded without errors</returns>
		protected abstract string OnAssetLoaded();

		protected virtual void OnAssetPathChanged(IStorageProvider oldProvider, string oldpath){}

		public void SaveToFile(IStorageProvider storageProvider, string filepath)
		{
			filepath = storageProvider.GetFullPath(filepath);
			if ((storageProvider!=CurrentStorageProvider) || !string.Equals(m_assetFilepath, filepath))
			{
				var oldPath = m_assetFilepath;
				var oldProvider = CurrentStorageProvider;
				CurrentStorageProvider = storageProvider;
				m_assetFilepath = filepath;
				OnAssetPathChanged(oldProvider, oldPath);
			}

			using (var f = CurrentStorageProvider.RequestFile(filepath, FileMode.Create, FileAccess.Write))
			{
				var serializer = new JSONSerializer();
				serializer.Serialize(f,this);
			}
		}

		protected string ToRelativePath(string absolutePath)
		{
			if (m_assetFilepath == null)
				return absolutePath;
			return CurrentStorageProvider.ToRelativePath(m_assetFilepath, absolutePath);
		}

		protected string ToAbsolutePath(string relativePath)
		{
			if (m_assetFilepath == null)
				return relativePath;
			return CurrentStorageProvider.ToAbsolutePath(m_assetFilepath, relativePath);
		}
	}
}