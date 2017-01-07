using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringTool
{
	public static class DialogUtilities
	{
		private static readonly HashAlgorithm _hashAlg = MD5.Create();
		private static readonly char[] _invalidChars = {'.','/','\\','*','"','[',']',':',';',',','|','=','\n','\t','\b','!','?'};
		private const int MAX_CHARACTERS = 20;

		private static string JoinStringArray(string[] strs)
		{
			switch (strs.Length)
			{
				case 0:
					return "-";
				case 1:
					return strs[0];
			}

			return strs.Aggregate((s, s1) => s + "," + s1);
		}

		public static string GenerateFileKey(DialogueStateActionDTO dto)
		{
			return $"{dto.CurrentState}#{dto.NextState}#{JoinStringArray(dto.Meaning)}({JoinStringArray(dto.Style)})".ToUpperInvariant();
		}

		public static string GenerateUtteranceFileName(string utterance)
		{
			var seq = utterance.Where(c => !_invalidChars.Contains(c)).Take(MAX_CHARACTERS).Select(char.ToUpperInvariant);
			var head = new string(seq.ToArray());

			var bytes = Encoding.UTF8.GetBytes(utterance);
			return head+"-"+BitConverter.ToString(_hashAlg.ComputeHash(bytes)).Replace("-", string.Empty);
		}
	}
}