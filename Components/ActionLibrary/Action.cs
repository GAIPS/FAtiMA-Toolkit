using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using WellFormedNames;

namespace ActionLibrary
{
	public class Action : IAction
	{
		public Name Key { get;}
		public Name Target { get;}
		public IList<Name> Parameters { get; }
		public float Utility { get; internal set; }
        public Name Name { get { return Name.BuildName(Parameters.Prepend(Key)); } }

        public Action(IEnumerable<Name> nameAndParameters, Name target)
		{
			var a = new List<Name>(nameAndParameters);
			Key = a[0];
			a.RemoveAt(0);
			Target = target;
			Parameters = a;
			Utility = 0;
		}

        public override string ToString()
        {
            return this.Name.ToString();
        }
	}
}