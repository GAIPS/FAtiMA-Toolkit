using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using IntegratedAuthoringToolWF.Properties;
using IntegratedAuthoringToolWF.TTSEngines;
using IntegratedAuthoringToolWF.TTSEngines.L2F;
using IntegratedAuthoringToolWF.TTSEngines.System;

namespace IntegratedAuthoringToolWF
{
	public partial class TextToSpeechForm : Form
	{
		private static readonly TextToSpeechEngine[] _ttsEngines =
		{
			new SystemTextToSpeechEngine(),
			new L2FSpeechEngine()
		};

		private readonly DialogueStateActionDTO[] _agentActions;
		private readonly IVoice[] _availableVoices;

		private IVoice _selectedVoice;
		private VoicePlayer _activeVoicePlayer;

		public TextToSpeechForm(DialogueStateActionDTO[] agentActions)
		{
			InitializeComponent();

			_agentActions = agentActions;

			_dialogOptions.DataSource = _agentActions.Select(a => a.Utterance).ToArray();
			_dialogOptions.SelectedIndex = -1;

			_availableVoices = _ttsEngines.SelectMany(e => e.GetAvailableVoices()).ToArray();
			_voiceComboBox.DataSource = _availableVoices.Select(VoiceToIdString).ToArray();
			_selectedVoice = _availableVoices[0];
			UpdateButtonText(true);

			UpdateRateLabel();
			UpdatePitchLabel();
			SetVisemeDisplay(Viseme.Silence);
		}

		private void _dialogOptions_SelectionChangeCommitted(object sender, EventArgs e)
		{
			textBox1.Text = _agentActions[_dialogOptions.SelectedIndex].Utterance;
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			_dialogOptions.SelectedIndex = -1;
		}

		private static string VoiceToIdString(IVoice info)
		{
			return $"{info.Name}, {info.Gender} - {info.Age}, {info.Culture} ({info.ParentEngine.Name})";
		}

		private void OnVoiceSelectionChange(object sender, EventArgs e)
		{
			_selectedVoice = _availableVoices[_voiceComboBox.SelectedIndex];
		}

		private void OnTestButtonClick(object sender, EventArgs e)
		{
			if (_activeVoicePlayer!=null)
			{
				_activeVoicePlayer.Stop();
				_activeVoicePlayer = null;
			}
			else
			{
				var a = _selectedVoice.BuildPlayer(textBox1.Text, GetRate(), GetPitch());
				if (a != null)
				{
					_voiceComboBox.Enabled = false;
					_generateAllButton.Enabled = false;
					_generateButton.Enabled = false;
					UpdateButtonText(false);
					_activeVoicePlayer = a;
					_activeVoicePlayer.Play(SynthesizerOnSpeakCompleted,OnVisemeHit);
				}
			}
		}

		private void OnVisemeHit(Viseme v)
		{
			if(_activeVoicePlayer==null)
				return;

			SetVisemeDisplay(v);
		}

		private void SynthesizerOnSpeakCompleted()
		{
			ThreadSafe(_voiceComboBox, c => c.Enabled = true);
			ThreadSafe(_generateAllButton, c => c.Enabled = true);
			ThreadSafe(_generateButton, b => b.Enabled=true );

			_activeVoicePlayer = null;
			UpdateButtonText(true);
			SetVisemeDisplay(Viseme.Silence);
		}

		private void SetVisemeDisplay(Viseme v)
		{
			Bitmap b;
			switch (v)
			{
				case Viseme.Silence:
					b = Resources.viseme_00;
					break;
				case Viseme.AxAhUh:
					b = Resources.viseme_01;
					break;
				case Viseme.Aa:
					b = Resources.viseme_02;
					break;
				case Viseme.Ao:
					b = Resources.viseme_03;
					break;
				case Viseme.EyEhAe:
					b = Resources.viseme_04;
					break;
				case Viseme.Er:
					b = Resources.viseme_05;
					break;
				case Viseme.YIyIhIx:
					b = Resources.viseme_06;
					break;
				case Viseme.WUwU:
					b = Resources.viseme_07;
					break;
				case Viseme.Ow:
					b = Resources.viseme_08;
					break;
				case Viseme.Aw:
					b = Resources.viseme_09;
					break;
				case Viseme.Oy:
					b = Resources.viseme_10;
					break;
				case Viseme.Ay:
					b = Resources.viseme_11;
					break;
				case Viseme.H:
					b = Resources.viseme_12;
					break;
				case Viseme.R:
					b = Resources.viseme_13;
					break;
				case Viseme.L:
					b = Resources.viseme_14;
					break;
				case Viseme.SZTs:
					b = Resources.viseme_15;
					break;
				case Viseme.ShChJhZh:
					b = Resources.viseme_16;
					break;
				case Viseme.ThDh:
					b = Resources.viseme_17;
					break;
				case Viseme.FV:
					b = Resources.viseme_18;
					break;
				case Viseme.DTDxN:
					b = Resources.viseme_19;
					break;
				case Viseme.KGNg:
					b = Resources.viseme_20;
					break;
				case Viseme.PBM:
					b = Resources.viseme_21;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(v), v, null);
			}
			_visemeDisplay.Image = b;
		}

		private void UpdateButtonText(bool state)
		{
			ThreadSafe(_testSpeechButton, c => c.Text = state ? "Speak Text" : "Stop");
		}

		private void OnRateValueChanged(object sender, EventArgs e)
		{
			UpdateRateLabel();
		}

		private void OnPitchValueChanged(object sender, EventArgs e)
		{
			UpdatePitchLabel();
		}

		private void UpdateRateLabel()
		{
			var r = GetRate();
			_rateTextBox.Text = $"{r:0.###}x";
		}

		private void UpdatePitchLabel()
		{
			var p = GetPitch();
			_pitchValueLabel.Text = p == 0 ? "0" : $"{p}st";
		}

		private double GetRate()
		{
			if (_speachRateSlider.InvokeRequired)
				return (double) _speachRateSlider.Invoke((Func<double>) GetRate);

			return _speachRateSlider.Value*0.001;
		}

		private void OnValidatedRateTextBox(object sender, EventArgs e)
		{
			var t = _rateTextBox.Text;
			var m = Regex.Match(t, "^((?:\\d*(?:\\.|,))?\\d+)x?$");
			if (m.Success)
			{
				double d;
				if (double.TryParse(m.Groups[1].Value.Replace('.', ','), out d))
				{
					var c = Clamp(d*1000, _speachRateSlider.Minimum, _speachRateSlider.Maximum);
					_speachRateSlider.Value = (int) Math.Round(c);
				}
			}

			UpdateRateLabel();
		}

		private static double Clamp(double value, double min, double max)
		{
			return value < min ? min : (value > max ? max : value);
		}

		private int GetPitch()
		{
			if (_pitchSlider.InvokeRequired)
				return (int) _pitchSlider.Invoke((Func<int>) GetPitch);

			return _pitchSlider.Value;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (_activeVoicePlayer != null)
				_activeVoicePlayer.Stop();
		}

		private void OnGenerateSingleButtonClick(object sender, EventArgs e)
		{
			var folder = new FolderBrowserDialog();
			folder.Description = "Select Output folder";
			if (folder.ShowDialog(this) != DialogResult.OK)
				return;

			var path = folder.SelectedPath;
			EditorUtilities.DisplayProgressBar("Generating TTS from Agent's Dialog Data", controler =>
			{
				GenerateSingleVoiceTask(path,controler);
			});
		}

		private void OnGenerateAllButtonClick(object sender, EventArgs e)
		{
			var folder = new FolderBrowserDialog();
			folder.Description = "Select Output folder";
			if (folder.ShowDialog(this) != DialogResult.OK)
				return;

			var path = folder.SelectedPath;
			EditorUtilities.DisplayProgressBar("Generating TTS from Agent's Dialog Data", controler =>
			{
				GenerateAllVoicesTask(path, controler);
			});
		}

		private void GenerateSingleVoiceTask(string path, IProgressBarControler controler)
		{
			var rate = GetRate();
			var pitch = GetPitch();

			string text;
			var i = ThreadSafe(_dialogOptions, d => d.SelectedIndex);
			if (i < 0)
			{
				text = textBox1.Text;
				path = Path.Combine(path, "preview");
			}
			else
			{
				var a = _agentActions[i];
				var id = DialogUtilities.GenerateFileKey(a);
				path = Path.Combine(path, id);
				text = a.Utterance;
			}

			controler.Message = $"Generating TTS for Utterance";
			BakeAndSaveTTS(path,text,rate,pitch);
			controler.Percent = 1;
		}

		private void GenerateAllVoicesTask(string basePath, IProgressBarControler controller)
		{
			var rate = GetRate();
			var pitch = GetPitch();

			var i = 0;
			foreach (var split in _agentActions.Zip(controller.Split(_agentActions.Length), (dto, ctrl) => new {data = dto, ctrl}))
			{
				var id = DialogUtilities.GenerateFileKey(split.data);
				var path = Path.Combine(basePath, id);
				split.ctrl.Message = $"Generating TTS for Utterance ({++i}/{_agentActions.Length})";
				BakeAndSaveTTS(path, split.data.Utterance,rate,pitch);
				split.ctrl.Percent = 1;
			}
			controller.Percent = 1;
		}

		private void BakeAndSaveTTS(string path, string utterance, double rate, int pitch)
		{
			var bake = _selectedVoice.BakeTTS(utterance, rate, pitch);
			if (bake == null)
				return;

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			var hash = DialogUtilities.UtteranceHash(utterance);
			var path2 = Path.Combine(path, hash.ToString());
			using (var audioFile = File.Open(path2 + ".wav", FileMode.Create, FileAccess.Write))
			{
				audioFile.Write(bake.waveStreamData, 0, bake.waveStreamData.Length);
			}

			using (var writer = new XmlTextWriter(path2 + ".xml", new UTF8Encoding(false)))
			{
				writer.Formatting = Formatting.Indented;
				writer.WriteStartDocument();
				writer.WriteStartElement("LipSyncVisemes");
				writer.WriteAttributeString("wavFile", hash + ".wav");

				double time = 0;
				foreach (var v in bake.visemes)
				{
					if (v.viseme > Viseme.Silence)
					{
						writer.WriteStartElement("viseme");
						writer.WriteAttributeString("type", ((sbyte)v.viseme).ToString());
						writer.WriteAttributeString("time", time.ToString(CultureInfo.InvariantCulture));
						writer.WriteAttributeString("duration", v.duration.ToString(CultureInfo.InvariantCulture));
						writer.WriteEndElement();
					}
					time += v.duration;
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Enter && AcceptButton == null)
			{
				var box = ActiveControl as TextBoxBase;
				if (box == null || !box.Multiline)
				{
					// Not a dialog, not a multi-line textbox; we can use Enter for tabbing
					SelectNextControl(ActiveControl, true, true, true, true);
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void ThreadSafe<T>(T control, Action<T> action) where T : Control
		{
			if (control.InvokeRequired)
			{
				Invoke(action, control);
			}
			else
			{
				action(control);
			}
		}

		private R ThreadSafe<T,R>(T control, Func<T,R> action) where T : Control
		{
			if (control.InvokeRequired)
			{
				return (R)Invoke(action, control);
			}
			else
			{
				return action(control);
			}
		}
	}
}