namespace GAIPS.AssetEditorTools
{
	public sealed class ConditionHolder
	{
		public string Condition { get; set; }

		public static explicit operator ConditionHolder(string value)
		{
			return new ConditionHolder() {Condition = value};
		}
	}
}