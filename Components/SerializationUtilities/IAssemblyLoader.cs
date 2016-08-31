using System;
using System.Collections.Generic;
using System.Reflection;

namespace SerializationUtilities
{
	public interface IAssemblyLoader
	{
		event Action OnAssemblyLoad;

		void OnBind();
		void OnUnbind();

		IEnumerable<Assembly> GetAssemblies();
	}
}