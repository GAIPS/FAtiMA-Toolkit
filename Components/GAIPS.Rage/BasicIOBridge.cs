using System;
using System.IO;
using AssetPackage;

namespace GAIPS.Rage
{
    public class BasicIOBridge : IBridge, AssetPackage.IDataStorage
    {
        public bool Delete(string fileId)
        {
            throw new InvalidOperationException();
        }

        public bool Exists(string fileId)
        {
            return File.Exists(fileId);
        }

        public string[] Files()
        {
            throw new InvalidOperationException();
        }

        public string Load(string fileId)
        {
            using (var reader = File.OpenText(fileId))
            {
                return reader.ReadToEnd();
            }
        }

        public void Save(string fileId, string fileData)
        {
            using (var writer = File.CreateText(fileId))
            {
                writer.Write(fileData);
            }
        }
    }
}