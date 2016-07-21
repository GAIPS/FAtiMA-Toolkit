namespace Conditions.DTOs
{
	/// <summary>
	/// Data Type Object Class for the representation of a condition set
	/// </summary>
	public class ConditionSetDTO
	{
		/// <summary>
		/// The logical quantifier of this condition set.
		/// Used to change how the entier condition set is evaluated.
		/// </summary>
		public LogicalQuantifier Quantifier { get; set; }
		/// <summary>
		/// The conditions to be evaluated as a single set.
		/// </summary>
		public string[] ConditionSet;
	}
}