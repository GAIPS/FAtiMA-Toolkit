using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using KnowledgeBase;
using KnowledgeBase.DTOs;

namespace GAIPS.AssetEditorTools.DynamicPropertiesWindow
{
	public partial class DynamicPropertyDisplayer : Form
	{
		private static DynamicPropertyDisplayer _instance;

		public static DynamicPropertyDisplayer Instance
		{
			get
			{
				if (_instance == null || _instance.IsDisposed)
					_instance = new DynamicPropertyDisplayer();

				return _instance;
			}
		}

		private DynamicPropertyRegistry _registry;

		private DynamicPropertyDisplayer()
		{
			InitializeComponent();

			_registry = new DynamicPropertyRegistry(name => false);

			Populate();
			AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
		}

		private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			RegistValidTypes(args.LoadedAssembly.GetTypes());
			UpdateView();
		}

		public void ShowOrBringToFront()
		{
			if (Visible)
				BringToFront();
			else
				Show();
		}

		private void Populate()
		{
			_registry.Clear();
			RegistValidTypes(AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()));
			UpdateView();
		}

		private void RegistValidTypes(IEnumerable<Type> allTypes)
		{
			var candidates = allTypes.Where(t => t.IsClass && !t.IsAbstract && typeof(IDynamicPropertiesRegister).IsAssignableFrom(t))
					.Select(t => FormatterServices.GetUninitializedObject(t) as IDynamicPropertiesRegister)
					.ToArray();

			foreach (var c in candidates)
			{
				try
				{
					c.BindToRegistry(_registry);
				}
				catch (Exception)
				{
					//Ignore exception
				}
			}
		}

		private void UpdateView()
		{
			var list = _registry.GetDynamicProperties().Select(e => new DynamicPropertyDTO()
			{
				PropertyTemplate = e.PropertyTemplate.ToString(),
				Description = e.Description
			}).OrderBy(dto => dto.PropertyTemplate).ToList();
			_dynamicPropertiesListView.DataSource = new BindingListView<DynamicPropertyDTO>(list);
			
			//_dynamicPropertiesListView.Columns[PropertyUtil.GetPropertyName<DynamicPropertyEntry>(dto => dto.Description)].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		}
	}
}
