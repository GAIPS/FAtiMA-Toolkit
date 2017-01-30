using WellFormedNames;

namespace KnowledgeBase
{
	public static class NameUtilities
	{
		public static Name ApplySelfPerspective(this Name original, Name name)
		{
			return original.SwapTerms(name, Name.SELF_SYMBOL);
		}

		public static Name RemoveSelfPerspective(this Name original, Name name)
		{
			return original.SwapTerms(Name.SELF_SYMBOL, name);
		}
	}
}