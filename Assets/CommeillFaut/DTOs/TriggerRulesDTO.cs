using System;
using System.Collections.Generic;

namespace CommeillFaut.DTOs
{
	/// <summary>
	/// Data Type Object Class for defining a Social Importance Asset components.
	/// </summary>
	[Serializable]
	public class TriggerRulesDTO
    {
		/// <summary>
		/// The set of attribution rules used to calculate Social importance values to targets
		/// </summary>
		public Dictionary<InfluenceRuleDTO, string> _TriggerRules;
	
		
	}
}