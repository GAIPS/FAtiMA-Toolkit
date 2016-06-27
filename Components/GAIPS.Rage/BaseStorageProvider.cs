using System;
using System.Diagnostics;
using System.IO;
using AssetManagerPackage;

namespace GAIPS.Rage
{
	public abstract class BaseStorageProvider : IStorageProvider
	{
		private readonly string m_root;

		protected BaseStorageProvider(string rootPath = null)
		{
			m_root = rootPath;
		}

		public Stream RequestFile(string filePath, FileMode mode, FileAccess access)
		{
			return LoadFile(GetFullURL(filePath), mode, access);
		}

		protected string GetFullURL(string path)
		{
			path = GetFullPath(path);
			if (m_root != null)
				path = m_root + path;
			return path;
		}

		protected abstract Stream LoadFile(string absoluteFilePath, FileMode mode, FileAccess access);

		private bool IsPathRooted(string path)
		{
			if (m_root == null)
				return Path.IsPathRooted(path);

			switch (path.Length)
			{
				case 0:
					return false;
				case 1:
					return IsDirSeparator(path[0]);
			}

			return IsDirSeparator(path[0]) && !IsDirSeparator(path[1]);
		}

		protected abstract bool IsDirectory(string path);

		protected static bool IsDirSeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		public string GetFullPath(string path)
		{
			if (IsPathRooted(path))
				return path;

			if (m_root == null)
				return Path.GetFullPath(path);

			return Path.Combine("/", path);
		}

		public string ToRelativePath(string basePath, string absolutePath)
		{
			if (!IsPathRooted(absolutePath))
				return absolutePath;

			var abs = new Uri(absolutePath);
			var origin = new Uri(basePath);
			return origin.MakeRelativeUri(abs).ToString();
		}

		public string ToAbsolutePath(string basePath, string relativePath)
		{
			if (!IsDirectory(basePath))
				basePath = Path.GetDirectoryName(basePath);

			return GetFullPath(Path.Combine(basePath, relativePath));
		}
	}
}