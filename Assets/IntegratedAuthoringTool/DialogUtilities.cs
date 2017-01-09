using System;
using System.Security.Cryptography;
using System.Text;
using Utilities;

namespace IntegratedAuthoringTool
{
	public static class DialogUtilities
	{
		private static readonly HashAlgorithm _hashAlg = MD5.Create();
		
		public static string GenerateUtteranceFileName(string utterance)
		{
			utterance.RemoveWhiteSpace();
            utterance = utterance.ToLowerInvariant();
			var bytes = Encoding.UTF8.GetBytes(utterance);
			return BitConverter.ToString(_hashAlg.ComputeHash(bytes));
		}
	}
}