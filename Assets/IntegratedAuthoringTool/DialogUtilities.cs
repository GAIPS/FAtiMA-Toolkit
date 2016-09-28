using System;
using System.Linq;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringTool
{
	public static class DialogUtilities
	{
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

		public static uint UtteranceHash(string utterance)
		{
			unchecked
			{
				return (uint) utterance.GetHashCode();
			}
		}
	}
}