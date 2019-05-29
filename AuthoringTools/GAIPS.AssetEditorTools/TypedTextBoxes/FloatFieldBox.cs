using System.Globalization;

namespace GAIPS.AssetEditorTools.TypedTextBoxes
{
	public sealed class FloatFieldBox : TypedFieldBox<float>
	{
		private static readonly SingleConvertionProvider PROVIDER = new SingleConvertionProvider();

        public bool HasBounds { get; set; } = false;
        public float MinValue { get; set; }
        public float MaxValue { get; set; }

        public FloatFieldBox() : base(0, PROVIDER)
		{
		}

        protected override bool ValidateValue(float value)
        {
            if (HasBounds && value < MinValue)
                return false;

            if (HasBounds && value > MaxValue)
                return false;

            return true;
        }

        private class SingleConvertionProvider : ITypeConversionProvider<float>
		{
			public bool TryToParseType(string str, out float value)
			{
				return float.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
			}

			public string ToString(float value)
			{
				return value.ToString();
			}
		}
	}
}