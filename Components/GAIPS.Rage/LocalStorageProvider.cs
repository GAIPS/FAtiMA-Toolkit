using System.IO;

namespace GAIPS.Rage
{
	public sealed class LocalStorageProvider : BaseStorageProvider
	{
		public static IStorageProvider Instance { get; } = new LocalStorageProvider();

		private LocalStorageProvider(){}

		protected override Stream LoadFile(string absoluteFilePath, FileMode mode, FileAccess access)
		{
			return File.Open(absoluteFilePath, mode, access);
		}

		protected override bool IsDirectory(string path)
		{
			var att = File.GetAttributes(path);
			return (att & FileAttributes.Directory) == FileAttributes.Directory;
		}
	}
}