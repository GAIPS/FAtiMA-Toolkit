namespace GAIPS.AssetEditorTools.TypedTextBoxes
{
	public sealed class Int32FieldBox : TypedFieldBox<int>
	{
		public Int32FieldBox() : base(0, FORMATTER)
		{
		}

		private static readonly Int32Converter FORMATTER = new Int32Converter();
		private class Int32Converter : ITypeConversionProvider<int>
		{
			public bool TryToParseType(string str, out int value)
			{
				return int.TryParse(str, out value);
			}

			public string ToString(int value)
			{
				return value.ToString();
			}
		}
	}
}