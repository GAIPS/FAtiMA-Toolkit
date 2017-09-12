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
					b = Resources._0_silence;
					break;
				case Viseme.AxAhUh:
					b = Resources._1_11_AxAhUh;
					break;
				case Viseme.Aa:
					b = Resources._2_Aa;
					break;
				case Viseme.Ao:
					b = Resources._3_Ao;
					break;
				case Viseme.EyEhAe:
                    b = Resources._4_EyEhAe;
					break;
				case Viseme.Er:
					b = Resources._5_Er;
					break;
				case Viseme.YIyIhIx:
					b = Resources._6_YIyIhIx;
					break;
				case Viseme.WUwU:
					b = Resources._7_WUwU;
					break;
				case Viseme.Ow:
					b = Resources._8_10_Ow;
					break;
				case Viseme.Aw:
					b = Resources._9_Aw;
					break;
				case Viseme.Oy:
					b = Resources._8_10_Ow;
					break;
				case Viseme.Ay:
					b = Resources._9_Aw;
					break;
				case Viseme.H:
					b = Resources._12_H;
					break;
				case Viseme.R:
					b = Resources._13_R;
					break;
				case Viseme.L:
					b = Resources._14_L;
					break;
				case Viseme.SZTs:
					b = Resources._15_SZTs;
					break;
				case Viseme.ShChJhZh:
					b = Resources._16_ShChJhZh;
					break;
				case Viseme.ThDh:
					b = Resources._17_ThDh;
					break;
				case Viseme.FV:
					b = Resources._18_FV;
					break;
				case Viseme.DTDxN:
                    b = Resources._19_DTDxN;
					break;
				case Viseme.KGNg:
					b = Resources._20_KGNg;
					break;
				case Viseme.PBM:
					b = Resources._21_PBM;
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
			}
			else
			{
				var a = _agentActions[i];
				text = a.Utterance;
			}

			controler.Message = $"Generating TTS for Utterance";
			BakeAndSaveTTS(path,text,rate,pitch);
			controler.Percent = 1;
		}

		private void GenerateAllVoicesTask(string path, IProgressBarControler controller)
		{
			var rate = GetRate();
			var pitch = GetPitch();

			var i = 0;
			foreach (var split in _agentActions.Zip(controller.Split(_agentActions.Length), (dto, ctrl) => new {data = dto, ctrl}))
			{
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

			var hash = IntegratedAuthoringToolAsset.GenerateUtteranceId(utterance);
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

        private void _dialogOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}