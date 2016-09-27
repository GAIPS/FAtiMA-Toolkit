using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF
{
	public partial class TextToSpeechForm : Form
	{
		private SpeechSynthesizer _synthesizer = new SpeechSynthesizer();
		private PromptBuilder _builder = new PromptBuilder();
		private VoiceInfo[] _installedVoices;

		private DialogueStateActionDTO[] _agentActions;
		private Prompt _current = null;

		public TextToSpeechForm(DialogueStateActionDTO[] agentActions)
		{
			InitializeComponent();

			_agentActions = agentActions;
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
				UpdateSpeechData(textBox1.Text);
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
			if (_speachRateSlider.InvokeRequired)
				return (double) _speachRateSlider.Invoke((Func<double>)GetRate);

			return Math.Pow(3, _speachRateSlider.Value * 0.01);
		}

		private int GetPitch()
		{
			if (_pitchSlider.InvokeRequired)
				return (int) _pitchSlider.Invoke((Func<int>) GetPitch);

			return _pitchSlider.Value;
		}

		private VoiceInfo GetCurrentlySelectedVoiceInfo()
		{
			if (_voiceComboBox.InvokeRequired)
				return (VoiceInfo)_voiceComboBox.Invoke((Func<VoiceInfo>) GetCurrentlySelectedVoiceInfo);

			return _installedVoices[_voiceComboBox.SelectedIndex];
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

			var path = Path.Combine(folder.SelectedPath, $"TTS Generation - {DateTime.UtcNow.ToString("hh-mm-ss-tt_dd-MM-yyyy")}");
			EditorUtilities.DisplayProgressBar("Generating TTS from Agent's Dialog Data", controler =>
			{
				Directory.CreateDirectory(path);
				GenerateVoicesTask(path,controler);
			});
		}

		private static string JoinStringArray(string[] strs)
		{
			switch (strs.Length)
			{
				case 0:
					return "-";
				case 1:
					return strs[0];
			}

			return strs.Aggregate((s, s1) => s + "," + s1);
		}

		private void GenerateVoicesTask(string basePath, IProgressBarControler controller)
		{
			int i = 0;
			foreach (var split in _agentActions.Zip(controller.Split(_agentActions.Length), (dto, ctrl) => new { data = dto, ctrl }))
			{
				var id = $"{split.data.CurrentState}#{split.data.NextState}#{JoinStringArray(split.data.Meaning)}({JoinStringArray(split.data.Style)})".ToUpperInvariant();
				var path = Path.Combine(basePath, id);
				Directory.CreateDirectory(path);

				split.ctrl.Message = $"Generating TTS for Utterance ({++i}/{_agentActions.Length})";
				GenerateTTS(path, split.data,split.ctrl);
				split.ctrl.Percent = 1;
			}
			controller.Percent = 1;
		}

		private void GenerateTTS(string path, DialogueStateActionDTO dto, IProgressBarControler ctrl)
		{
			List<Tuple<int, TimeSpan>> visemes = new List<Tuple<int, TimeSpan>>();
			EventHandler<VisemeReachedEventArgs> visemeHandler = (o, args) =>
			{
				visemes.Add(Tuple.Create(args.Viseme, args.Duration));
			};

			_synthesizer.VisemeReached += visemeHandler;
			UpdateSpeechData(dto.Utterance);
			_synthesizer.SetOutputToWaveFile(Path.Combine(path, "speech.wav"));
			_synthesizer.Speak(_builder);
			_synthesizer.SetOutputToDefaultAudioDevice();
			_synthesizer.VisemeReached -= visemeHandler;

			ctrl.Percent = 0.5f;

			if (visemes.Count == 0)
			{
				//No visemes were generated. This means that no speach was produced by the synthesizer.
				//Delete folder and data, and ignore this text.

				Directory.Delete(path,true);
				return;
			}

			using (var writer = new XmlTextWriter(Path.Combine(path, "speech.xml"), new UTF8Encoding(false)))
			{
				writer.Formatting = Formatting.Indented;
				writer.WriteStartDocument();
				writer.WriteStartElement("LipSyncVisemes");
				writer.WriteAttributeString("wavFile", "speech.wav");

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
		}

		private void SynthesizerOnSpeakCompleted(object sender, SpeakCompletedEventArgs speakCompletedEventArgs)
		{
			_current = null;
			_synthesizer.SpeakCompleted -= SynthesizerOnSpeakCompleted;
			UpdateButtonText(true);
		}

		private void UpdateSpeechData(string text)
		{
			_builder.ClearContent();

			var voice = GetCurrentlySelectedVoiceInfo();
			_builder.StartVoice(voice);
			var prosody = $"<prosody rate=\"{GetRate()}\" pitch=\"{GetPitch()}st\">";
			_builder.AppendSsmlMarkup(prosody);
			_builder.AppendText(text);
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
