namespace GAIPS.AssetEditorTools
{
	public interface ITypeConversionProvider<T>
	{
		bool TryToParseType(string str, out T value);
		string ToString(T value);
	}
}