using System;
using System.Collections.Generic;
using System.Reflection;

namespace SerializationUtilities
{
	public interface IAssemblyLoader
	{
		event Action<Assembly> OnAssemblyLoaded;

		void OnBind();
		void OnUnbind();

		IEnumerable<Assembly> GetAssemblies();
	}
}