using System.IO;
using GAIPS.Serialization;
using SocialImportance.DTOs;

namespace SocialImportance
{
	public static class DTOLoader
	{
		public static SocialImportanceDTO Load(Stream stream)
		{
			var serializer = new JSONSerializer();
			return serializer.Deserialize<SocialImportanceDTO>(stream);
		}

		public static void Save(SocialImportanceDTO dto, Stream stream)
		{
			var serializer = new JSONSerializer();
			serializer.Serialize(stream, dto);
		}
	}
}