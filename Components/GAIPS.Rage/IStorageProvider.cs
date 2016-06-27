using System.IO;

namespace GAIPS.Rage
{
	public interface IStorageProvider
	{
		Stream RequestFile(string filePath, FileMode mode, FileAccess access);

		string GetFullPath(string path);

		string ToRelativePath(string basePath, string absolutePath);
		string ToAbsolutePath(string basePath, string relativePath);
	}
}