using System;
using SerializationUtilities;
using Utilities.Json;

namespace GAIPS.Rage
{
	public abstract class Asset<T> : IAsset where T : Asset<T>, new ()
	{
		protected static readonly JSONSerializer SERIALIZER = new JSONSerializer();

        private AssetStorage storage;

		[NonSerialized] private string m_assetFilepath = null;
		public string AssetFilePath => m_assetFilepath;

        public static T CreateInstance(AssetStorage storage)
        {
            var config = storage.GetComponentConfiguration(typeof(T).Name);
            if (config == null)
            {
                var res = new T();
                res.storage = storage;
                res.Save();
                return res;
            }
            else
            {
                var aux = (JsonObject)JsonParser.Parse(config);
                var res = SERIALIZER.DeserializeFromJson<T>(aux);
                res.storage = storage;
                return res;
            }
        }

        public void Save()
        {
            var aux = SERIALIZER.SerializeToJson(this).ToString(true);
            this.storage.StoreComponent(typeof(T).Name, aux);
        }
	}
}