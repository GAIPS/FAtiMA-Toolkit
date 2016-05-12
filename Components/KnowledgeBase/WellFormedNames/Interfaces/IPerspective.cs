namespace KnowledgeBase.WellFormedNames.Interfaces
{
	public interface IPerspective<out T> where T : IPerspective<T>
	{
		T ApplyPerspective(Name name);
		T RemovePerspective(Name name);

		T SwapPerspective(Name oldPerspective, Name newPerspective);
	}
}