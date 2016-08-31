using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using SerializationUtilities;

namespace Test.SerializationUtilities
{
	public class InstanceFactoryImplementation : IInstanceFactory
	{
		public object CreateUninitialized(Type type)
		{
			return FormatterServices.GetSafeUninitializedObject(type);
		}
	}

	public class TestAssemblyLoader : IAssemblyLoader
	{
		public event Action OnAssemblyLoad;

		private void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			OnAssemblyLoad?.Invoke();
		}

		public void OnBind()
		{
			AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;
		}

		public void OnUnbind()
		{
			AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;
		}

		public IEnumerable<Assembly> GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}
	}
}