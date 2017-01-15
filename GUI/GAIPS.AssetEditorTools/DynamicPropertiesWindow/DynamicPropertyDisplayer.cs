using System.Windows.Forms;
using Equin.ApplicationFramework;

namespace GAIPS.AssetEditorTools.DynamicPropertiesWindow
{
	public partial class DynamicPropertyDisplayer : Form
	{
		#region Singleton

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

		private DynamicPropertyDisplayer()
		{
			InitializeComponent();
			UpdateView();
		}

		#endregion

		private static readonly DynamicPropertyEntry[] DYN_PROPERTIES = {
			new DynamicPropertyEntry()
			{
				Origin = "KB",
				PropertyTemplate = "Count([x])",
				Description = "The number of substitutions found for [x]."
			},
			new DynamicPropertyEntry()
			{
				Origin = "Emotional Appraisal Asset",
				PropertyTemplate = "EmotionIntensity([x], [y])",
				Description = "The intensity value for the emotion felt by agent [x] of type [y]."
			},
			new DynamicPropertyEntry()
			{
				Origin = "AM",
				PropertyTemplate = "EventElapsedTime([id])",
				Description = "The number of ticks passed since the event associated to [id] occured."
			},
			new DynamicPropertyEntry()
			{
				Origin = "AM",
				PropertyTemplate = "EventId([type], [subject], [def], [target])",
				Description = "Returns the ids of all events that unify with the property's name."
			},
			new DynamicPropertyEntry()
			{
				Origin = "AM",
				PropertyTemplate = "LastEventId([type], [subject], [def], [target])",
				Description = "Returns the id of the last event if it unifies with the property's name."
			},
			new DynamicPropertyEntry()
			{
				Origin = "Emotional Appraisal Asset",
				PropertyTemplate = "Mood([x])",
				Description = "The current mood value for agent [x]."
			},
			new DynamicPropertyEntry()
			{
				Origin = "Social Importance Asset",
				PropertyTemplate = "SI([target])",
				Description = "The value of Social Importance attributed to [target]."
			},
			new DynamicPropertyEntry()
			{
				Origin = "Emotional Appraisal Asset",
				PropertyTemplate = "StrongestEmotion([x])",
				Description = "The type of the current strongest emotion that agent [x] is feeling."
			},
			new DynamicPropertyEntry()
			{
				Origin = "Integrated Authoring Tool Asset",
				PropertyTemplate = "ValidDialogue([currentState], [nextState], [meaning], [style])",
				Description = "No description."
			},
			new DynamicPropertyEntry()
			{
				Origin = "Role Play Character Asset",
				PropertyTemplate = "IsAgent([x])",
				Description = "Given a name [x], returns true if that name is binded to an agent."
			}
		};

		public void ShowOrBringToFront()
		{
			if (Visible)
				BringToFront();
			else
				Show();
		}

		private void UpdateView()
		{
			_dynamicPropertiesListView.DataSource = new BindingListView<DynamicPropertyEntry>(DYN_PROPERTIES);
		}
	}
}
