namespace KnowledgeBase
{
	public interface IDynamicPropertiesRegister
	{
		void BindToRegistry(IDynamicPropertiesRegistry registry);
		void UnbindToRegistry(IDynamicPropertiesRegistry registry);
	}
}