namespace Utilities
{
	public static class Util
	{
		public static void Swap<T>(ref T v1, ref T v2)
		{
			T aux = v1;
			v1 = v2;
			v2 = aux;
		}
	}
}