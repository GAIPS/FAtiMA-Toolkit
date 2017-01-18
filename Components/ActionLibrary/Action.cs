using System;
using System.Collections.Generic;
using Utilities;
using WellFormedNames;

namespace ActionLibrary
{
	internal class Action : IAction
	{
		public Name ActionName { get;}
		public Name Target { get;}
		public IList<Name> Parameters { get; }
		public float Utility { get; internal set; }
        public Name FullName { get { return Name.BuildName(Parameters.Prepend(ActionName)); } }

        public Action(IEnumerable<Name> nameAndParameters, Name target)
		{
			var a = new List<Name>(nameAndParameters);
			ActionName = a[0];
			a.RemoveAt(0);
			Target = target;
			Parameters = a;
			Utility = 0;
		}

	}
}