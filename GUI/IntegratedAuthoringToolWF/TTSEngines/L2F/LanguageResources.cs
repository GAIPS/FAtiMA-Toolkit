using System.Collections.Generic;

namespace IntegratedAuthoringToolWF.TTSEngines.L2F
{
	public static class LanguageResources
	{
		public static readonly Dictionary<string, Viseme> PT_PhonesToVisemes = new Dictionary<string, Viseme>()
		{
			//{"#","#"},
			//{"@","@"},

			{"f",Viseme.FV},
			{"v",Viseme.FV},

			{"k",Viseme.KGNg},
			{"g",Viseme.KGNg},
			{"L",Viseme.KGNg},
			{"J",Viseme.KGNg},

			{"l",Viseme.L},
			{"l~",Viseme.L},
			{"R",Viseme.L},
			{"r",Viseme.L},

			{"O",Viseme.Ow},

			{"p",Viseme.PBM},
			{"b",Viseme.PBM},
			{"m",Viseme.PBM},

			{"s",Viseme.SZTs},
			{"z",Viseme.SZTs},
			{"S",Viseme.SZTs},
			{"Z",Viseme.SZTs},

			{"t",Viseme.DTDxN},
			{"d",Viseme.DTDxN},
			{"n",Viseme.DTDxN},

			{"a",Viseme.Aa},
			{"6",Viseme.AxAhUh},
			{"6~",Viseme.AxAhUh},
			{"6~j~",Viseme.Aa},
			{"6~w~",Viseme.Aw},

			{"e",Viseme.EyEhAe},
			{"E",Viseme.EyEhAe},
			{"e~",Viseme.EyEhAe},

			{"i",Viseme.YIyIhIx},
			{"i~",Viseme.YIyIhIx},
			{"j",Viseme.YIyIhIx},
			{"j~",Viseme.YIyIhIx},

			{"o",Viseme.Ow},
			{"o~",Viseme.Ow},
			{"o~j~",Viseme.Ow},

			{"u",Viseme.WUwU},
			{"u~",Viseme.WUwU},
			{"w",Viseme.WUwU},
			{"w~",Viseme.WUwU},
			{"u~j~",Viseme.WUwU}
		};
	}
}