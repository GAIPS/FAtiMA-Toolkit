using System;

namespace Conditions.DTOs
{
	/// <summary>
	/// Data Type Object Class for the representation of a condition set
	/// </summary>
    [Serializable]
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


        public override string ToString()
        {
            if (this.ConditionSet == null) return "-";

            string ret = "";
            
            foreach(var c in ConditionSet)
                ret += c.ToString() + ", ";


            ret = ret.TrimEnd();
            ret = ret.TrimEnd(',');
            return ret;
        }
	}
}