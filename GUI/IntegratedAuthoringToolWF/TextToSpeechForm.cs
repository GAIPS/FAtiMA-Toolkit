using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace IntegratedAuthoringToolWF
{
	public partial class TextToSpeechForm : Form
	{
		private SpeechSynthesizer _synthesizer = new SpeechSynthesizer();
		private PromptBuilder _builder = new PromptBuilder();
		private VoiceInfo[] _installedVoices;
		private Prompt _current = null;

		public TextToSpeechForm()
		{
			InitializeComponent();

			_installedVoices = _synthesizer.GetInstalledVoices().Where(v => v.Enabled).Select(v => v.VoiceInfo).ToArray();
			_voiceComboBox.DataSource = _installedVoices.Select(VoiceToIdString).ToArray();
			UpdateButtonText(true);

			_rateValueLabel.Text = GetRate().ToString();
			UpdatePitchLabel();
		}

		private static string VoiceToIdString(VoiceInfo info)
		{
			return $"{info.Name}, {info.Gender} - {info.Age}, {info.Culture.Name}";
		}

		private void OnTestButtonClick(object sender, EventArgs e)
		{
			if (_synthesizer.State == SynthesizerState.Speaking)
			{
				_synthesizer.SpeakAsyncCancel(_current);
				_current = null;
				_synthesizer.SpeakCompleted -= SynthesizerOnSpeakCompleted;
				UpdateButtonText(true);
			}
			else
			{ 
				UpdateSpeechData();

				_synthesizer.SetOutputToDefaultAudioDevice();
				_current = _synthesizer.SpeakAsync(_builder);
				_synthesizer.SpeakCompleted += SynthesizerOnSpeakCompleted;
				UpdateButtonText(false);
			}
		}

		private void UpdateButtonText(bool state)
		{
			button1.Text = state ? "Speak Text" : "Stop";
		}

		private void OnRateValueChanged(object sender, EventArgs e)
		{
			_rateValueLabel.Text = GetRate().ToString();
		}

		private void OnPitchValueChanged(object sender, EventArgs e)
		{
			UpdatePitchLabel();
		}

		private void UpdatePitchLabel()
		{
			var p = GetPitch();
			_pitchValueLabel.Text = p == 0 ? "0" : $"{p}st";
		}

		private double GetRate()
		{
			return Math.Pow(3, _speachRateSlider.Value * 0.01);
		}

		private int GetPitch()
		{
			return _pitchSlider.Value;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (_synthesizer.State != SynthesizerState.Speaking)
				return;

			_synthesizer.SpeakAsyncCancel(_current);
			_current = null;
		}

		private void OnGenerateButtonClick(object sender, EventArgs e)
		{
			var folder = new FolderBrowserDialog();
			folder.Description = "Select Output folder";
			if (folder.ShowDialog(this) != DialogResult.OK)
				return;

			List<Tuple<int,TimeSpan>> visemes = new List<Tuple<int, TimeSpan>>();
			EventHandler<VisemeReachedEventArgs> visemeHandler = (o, args) =>
			{
				visemes.Add(Tuple.Create(args.Viseme, args.Duration));
			};

			_synthesizer.VisemeReached += visemeHandler;
			UpdateSpeechData();
			_synthesizer.SetOutputToWaveFile(Path.Combine(folder.SelectedPath, "speech.wav"));
			_synthesizer.Speak(_builder);
			_synthesizer.SetOutputToDefaultAudioDevice();
			_synthesizer.VisemeReached -= visemeHandler;

			using (var writer = new XmlTextWriter(Path.Combine(folder.SelectedPath, "speech.xml"),Encoding.UTF8))
			{
				writer.Formatting = Formatting.Indented;
				writer.WriteStartDocument();
				writer.WriteStartElement("LipSyncVisemes");
				writer.WriteAttributeString("wavFile","speech.wav");

				double time = 0;
				foreach (var v in visemes)
				{
					var duration = v.Item2.TotalSeconds;
					if (v.Item1 != 0)
					{
						writer.WriteStartElement("viseme");
						writer.WriteAttributeString("type", v.Item1.ToString());
						writer.WriteAttributeString("time", time.ToString(CultureInfo.InvariantCulture));
						writer.WriteAttributeString("duration", duration.ToString(CultureInfo.InvariantCulture));
						writer.WriteEndElement();
					}
					time += duration;
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
			}

			Console.WriteLine("Done");
		}

		private void SynthesizerOnSpeakCompleted(object sender, SpeakCompletedEventArgs speakCompletedEventArgs)
		{
			_current = null;
			_synthesizer.SpeakCompleted -= SynthesizerOnSpeakCompleted;
			UpdateButtonText(true);
		}

		private void UpdateSpeechData()
		{
			_builder.ClearContent();

			var voice = _installedVoices[_voiceComboBox.SelectedIndex];
			_builder.StartVoice(voice);
			var prosody = $"<prosody rate=\"{GetRate()}\" pitch=\"{GetPitch()}st\">";
			_builder.AppendSsmlMarkup(prosody);
			_builder.AppendText(textBox1.Text);
			_builder.AppendSsmlMarkup("</prosody>");
			_builder.EndVoice();
		}

		private string IntToViseme(int value)
		{
			switch (value)
			{
				case 0:
					return "silence";
				case 1:
					return "ae ax ah";
				case 2:
					return "aa";
				case 3:
					return "ao";
				case 4:
					return "ey eh uh";
				case 5:
					return "er";
				case 6:
					return "y iy ih ix";
				case 7:
					return "w uw";
				case 8:
					return "ow";
				case 9:
					return "aw";
				case 10:
					return "oy";
				case 11:
					return "ay";
				case 12:
					return "h";
				case 13:
					return "r";
				case 14:
					return "l";
				case 15:
					return "s z";
				case 16:
					return "sh ch jh zh";
				case 17:
					return "th dh";
				case 18:
					return "f v";
				case 19:
					return "d t n";
				case 20:
					return "k g ng";
				case 21:
					return "p b m";
			}

			return "unknown";
		}
	}
}
