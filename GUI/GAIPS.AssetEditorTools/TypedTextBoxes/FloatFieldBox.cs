namespace GAIPS.AssetEditorTools.TypedTextBoxes
{
	public sealed class FloatFieldBox : TypedFieldBox<float>
	{
		private static readonly SingleConvertionProvider PROVIDER = new SingleConvertionProvider();

		public FloatFieldBox() : base(0, PROVIDER)
		{
		}

		private class SingleConvertionProvider : ITypeConversionProvider<float>
		{
			public bool TryToParseType(string str, out float value)
			{
				return float.TryParse(str, out value);
			}

			public string ToString(float value)
			{
				return value.ToString();
			}
		}
	}
}