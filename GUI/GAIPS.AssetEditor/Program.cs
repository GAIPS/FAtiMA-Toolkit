using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AssetManagerPackage;
using GAIPS.AssetEditor.Core;
using GAIPS.AssetEditor.Properties;

namespace GAIPS.AssetEditor
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string editorDllsFolder = Resources.DEFAULT_DLL_DIRECTORY;

			if (args.Length > 0)
			{
				if (string.Equals(args[0], "-e", StringComparison.InvariantCultureIgnoreCase))
				{
					editorDllsFolder = args[1];
					if (string.Equals(editorDllsFolder, "$CurrentDir"))
						editorDllsFolder = Directory.GetCurrentDirectory();
				}
			}

			if (!Directory.Exists(editorDllsFolder))
			{
				MessageBox.Show($"Unable to find \"{Resources.DEFAULT_DLL_DIRECTORY}\" folder.",Resources.INTERNAL_ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
				
			var dlls = Directory.GetFiles(editorDllsFolder, "*.dll");
			if (dlls.Length == 0)
			{
				MessageBox.Show($"Unable to find editor libraries within the \"{Resources.DEFAULT_DLL_DIRECTORY}\" folder.", Resources.NO_EDITOR_DLL_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
			try
			{
				
				//AppDomain.CurrentDomain.Load()
				var loadedAssemblies = dlls.Select(path => Assembly.LoadFile(Path.GetFullPath(path))).ToArray();
				var validTypes = loadedAssemblies.SelectMany(a => a.GetExportedTypes())
					.Where(t => typeof (Form).IsAssignableFrom(t) && !t.IsAbstract)
					.Where(t => t.GetCustomAttributes<MainEditorAttribute>(true).Any()).ToArray();

				if (validTypes.Length == 0)
				{
					MessageBox.Show("Unable to find a defined  main editor class inside the loaded assemblies.",
						"Unable to start editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				AssetManager.Instance.Bridge = new ApplicationBridge();

				if (validTypes.Length > 1)
				{
					throw new NotImplementedException();
				}

				var form = (Form)Activator.CreateInstance(validTypes[0]);
				Application.Run(form);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, Resources.INTERNAL_ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			var b = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
			return b;
		}
	}
}
