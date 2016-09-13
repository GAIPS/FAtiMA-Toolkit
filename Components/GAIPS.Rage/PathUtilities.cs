using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Utilities;

namespace GAIPS.Rage
{
	internal static class PathUtilities
	{
		public const char DirectorySeparatorChar = '\\';
		public const char AltDirectorySeparatorChar = '/';
		////private static bool IsDirChar(char c)
		//{
		//	return c == System.IO.Path.DirectorySeparatorChar || c == System.IO.Path.AltDirectorySeparatorChar;
		//}

		//public static bool IsPathRooted(string path)
		//{
		//	if (IsDirChar(path[0]))
		//		return true;

		//	if (char.IsLetter(path, 0) && path[1] == ':' && IsDirChar(path[3]))
		//		return true;

		//	return false;
		//}

		//public static bool HasExtension(string path)
		//{

		//}

		public static string CleanCombine(string basePath, string relativePath)
		{
			var result = basePath;
			var dirs = relativePath.Split(DirectorySeparatorChar,AltDirectorySeparatorChar);
			foreach (var d in dirs)
			{
				if (string.IsNullOrEmpty(d))
					continue;

				if (d == "..")
					result = Path.GetDirectoryName(result);
				else
					result += DirectorySeparatorChar + d;
			}

			return result;
		}

		public static string RelativePath(string absolutePath, string relativeTo)
		{
			string[] relativeDirectories = relativeTo.Split(DirectorySeparatorChar, AltDirectorySeparatorChar);
			string[] absoluteDirectories = absolutePath.Split(DirectorySeparatorChar, AltDirectorySeparatorChar);

			//Get the shortest of the two paths
			int length = relativeDirectories.Length < absoluteDirectories.Length ? relativeDirectories.Length : absoluteDirectories.Length;

			//Use to determine where in the loop we exited
			int lastCommonRoot = -1;
			int index;

			//Find common root
			for (index = 0; index < length; index++)
				if (relativeDirectories[index] == absoluteDirectories[index])
					lastCommonRoot = index;
				else
					break;

			//If we didn't find a common prefix then throw
			if (lastCommonRoot < 0)
				return absolutePath;

			//Build up the relative path

			StringBuilder relativePath = ObjectPool<StringBuilder>.GetObject();
			try
			{
				//Add on the ..
				for (index = lastCommonRoot + 1; index < relativeDirectories.Length; index++)
					if (relativeDirectories[index].Length > 0)
						relativePath.Append("..").Append(DirectorySeparatorChar);

				//Add on the folders
				for (index = lastCommonRoot + 1; index < absoluteDirectories.Length - 1; index++)
					relativePath.Append(absoluteDirectories[index]).Append(DirectorySeparatorChar);

				relativePath.Append(absoluteDirectories[absoluteDirectories.Length - 1]);

				return relativePath.ToString();
			}
			finally
			{
				relativePath.Length = 0;
				ObjectPool<StringBuilder>.Recycle(relativePath);
			}
		}
	}
}