using System;
using WellFormedNames;

namespace GAIPS.AssetEditorTools.TypedTextBoxes
{
	public class WFNameFieldBox : TypedFieldBox<Name>
	{
        public bool OnlyIntOrVariable { get; set; } = false;
        public bool AllowVariable { get; set; } = true;
		public bool AllowLiteral { get; set; } = true;
        public bool AllowComposedName { get; set; } = true;
        public bool AllowUniversal { get; set; } = true;
        public bool AllowUniversalLiteral { get; set; } = true;
        public bool AllowNil { get; set; } = true;

        public WFNameFieldBox() : base(WellFormedNames.Name.NIL_SYMBOL, FORMATTER){}
        
		protected override bool ValidateValue(Name value)
		{
            if (OnlyIntOrVariable)
            {
                if (value.IsVariable)
                    return true;
                else
                {
                    int res;
                    return int.TryParse(value.ToString(), out res);
                }
            }

			if (!AllowVariable && !value.IsGrounded)
				return false;

			if (!AllowUniversal && value.IsUniversal)
				return false;

            if (!AllowUniversalLiteral)
            {
                foreach(var lit in value.GetLiterals())
                {
                    if (lit.IsUniversal)
                    {
                        return false;
                    }
                }
            }

			if (!AllowLiteral && value.IsPrimitive)
				return false;

            if (!AllowComposedName && value.IsComposed)
                return false;

            if (!AllowNil && value == WellFormedNames.Name.NIL_SYMBOL)
                return false;

            return true;
		}

		private static readonly NameConverter FORMATTER = new NameConverter();
		private class NameConverter : ITypeConversionProvider<Name>
		{
			public bool TryToParseType(string str, out Name value)
			{
				value = null;
				try
				{
					value = WellFormedNames.Name.BuildName(str);
				}
				catch (Exception)
				{
					return false;
				}
				return true;
			}

		public string ToString(Name value)
			{
				return value?.ToString();
			}
		}
	}
}