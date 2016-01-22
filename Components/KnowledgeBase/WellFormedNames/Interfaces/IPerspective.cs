namespace KnowledgeBase.WellFormedNames.Interfaces
{
	public interface IPerspective<T> where T : IPerspective<T>
	{
		T ApplyPerspective(string name);
		T RemovePerspective(string name);
	}
}