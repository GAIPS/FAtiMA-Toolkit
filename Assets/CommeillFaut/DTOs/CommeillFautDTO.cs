using System;

namespace CommeillFaut.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a Social Importance Asset components.
	/// </summary>
	[Serializable]
	public class CommeillFautDTO
    {
		/// <summary>
		/// The set of attribution rules used to calculate Social importance values to targets
		/// </summary>
		public SocialExchangeDTO[] _SocialExchangesDtos;

    }
}