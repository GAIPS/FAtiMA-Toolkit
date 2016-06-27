namespace GAIPS.AssetEditorTools.TypedTextBoxes
{
	public class UInt32FieldBox : TypedFieldBox<uint>
	{
		public UInt32FieldBox() : base(0, FORMATTER)
		{
		}

		private static readonly UintConverter FORMATTER = new UintConverter();
		private class UintConverter : ITypeConversionProvider<uint>
		{
			#region Implementation of ITypeConversionProvider<uint>

			public bool TryToParseType(string str, out uint value)
			{
				return uint.TryParse(str, out value);
			}

			public string ToString(uint value)
			{
				return value.ToString();
			}

			#endregion
		}
	}
}