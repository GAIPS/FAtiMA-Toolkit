using WellFormedNames;

namespace KnowledgeBase
{
	public static class NameUtilities
	{
		public static Name ApplyPerspective(this Name original, Name name)
		{
			return original.SwapTerms(name, Name.SELF_SYMBOL);
		}

		public static Name RemovePerspective(this Name original, Name name)
		{
			return original.SwapTerms(Name.SELF_SYMBOL, name);
		}
	}
}