﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace SerializationUtilities
{
	internal class DefaultAssemblyLoader : IAssemblyLoader
	{
		public event Action OnAssemblyLoad;

#if !PORTABLE
		
		private void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			OnAssemblyLoad?.Invoke();
		}

#endif

		public void OnBind()
		{
#if !PORTABLE
			AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;
#endif
		}

		public void OnUnbind()
		{
#if !PORTABLE
			AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomainOnAssemblyLoad;
#endif
		}

		public IEnumerable<Assembly> GetAssemblies()
		{
#if PORTABLE
			yield return typeof (SerializationServices).GetAssembly();
#else
			return AppDomain.CurrentDomain.GetAssemblies();
#endif
		}
	}
}