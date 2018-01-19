using System;

namespace RolePlayCharacter
{
    /// <summary>
    /// Data Type Object Class for the representation of a belief that the asset has about a property of the world
    /// </summary>
    [Serializable]
    public class BeliefDTO
    {
		/// <summary>
		/// The name of the property that this belief refers.
		/// </summary>
        public string Name { get; set; }
		/// <summary>
		/// The value that is believed that this property has.
		/// </summary>
		public string Value { get; set; }

        public float Certainty { get; set; }

        /// <summary>
        /// From which perspective does this property holds this value. 
        /// </summary>
        /// <example>
        /// I belief that this sky is blue.
        /// Name = <b>Color(Sky)</b>
        /// Value = <b>Blue</b>
        /// Perspective = <b>SELF</b>
        /// I belief that John believes that the sky is green.
        /// Name = <b>Color(Sky)</b>
        /// Value = <b>Green</b>
        /// Perspective = <b>John</b>
        /// </example>
        public string Perspective { get; set; }
    }
}
