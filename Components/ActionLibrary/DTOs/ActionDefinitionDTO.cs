using System;
using KnowledgeBase.DTOs.Conditions;

namespace ActionLibrary.DTOs
{
	public class ActionDefinitionDTO
	{
		public Guid Id { get; set; }
		public string Action { get; set; }
		public string Target { get; set; }
		public ConditionSetDTO Conditions { get; set; }
	}
}