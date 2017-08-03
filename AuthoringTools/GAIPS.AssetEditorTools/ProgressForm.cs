using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAIPS.AssetEditorTools
{
	internal partial class ProgressForm : Form
	{
		private Action<IProgressBarControler> _task;
		private Thread _taskThread;

		public ProgressForm(string title, Action<IProgressBarControler> task)
		{
			InitializeComponent();
			_task = task;

			_taskThread = new Thread(ThreadStart);

			CenterToScreen();
			this.Text = title;
		}

		private void OnShown(object sender, EventArgs e)
		{
			_taskThread.Start();
		}

		private void ThreadStart()
		{
			var c = new RootBarControler(this);
			_task(c);

			Invoke(new Action(Close));
		}

		private float GetPercent()
		{
			if (_bar.InvokeRequired)
				return (float)_bar.Invoke(new Func<float>(GetPercent));

			return (_bar.Value - _bar.Minimum) / (float)(_bar.Maximum - _bar.Minimum);
		}

		private void SetPercent(float value)
		{
			if (_bar.InvokeRequired)
			{
				_bar.Invoke(new Action<float>(SetPercent), value);
				return;
			}
			_bar.Value = (int)(_bar.Minimum + value*(_bar.Maximum - _bar.Minimum));
		}

		private string GetMessage()
		{
			if (_progressMessage.InvokeRequired)
				return (string)_progressMessage.Invoke(new Func<string>(GetMessage));

			return _progressMessage.Text;
		}

		private void SetMessage(string value)
		{
			if (_progressMessage.InvokeRequired)
			{
				_progressMessage.Invoke(new Action<string>(SetMessage),value);
				return;
			}

			_progressMessage.Text = value;
		}

		private class RootBarControler : IProgressBarControler
		{
			private ProgressForm _parentForm;

			public RootBarControler(ProgressForm form)
			{
				_parentForm = form;
			}

			public float Percent {
				get
				{
					return _parentForm.GetPercent();
				}
				set
				{
					_parentForm.SetPercent(value < 0 ? 0 : (value > 1 ? 1 : value));
				}
			}
			public string Message {
				get { return _parentForm.GetMessage(); }
				set { _parentForm.SetMessage(value);}
			}

			public IEnumerable<IProgressBarControler> Split(int amount)
			{
				if(amount<1)
					throw new ArgumentException("Must be greater that 0",nameof(amount));

				var min = Percent;
				var diff = 1f - Percent;
				var steps = diff/amount;
				for (int i = 0; i < amount; i++)
				{
					yield return new SubBarControler(this, min+steps*i,steps);
				}
			}
		}

		private class SubBarControler : IProgressBarControler
		{
			private IProgressBarControler _parent;
			private float _min;
			private float _diff;

			public SubBarControler(IProgressBarControler parent, float min, float diff)
			{
				_parent = parent;
				_min = min;
				_diff = diff;
			}


			public float Percent {
				get
				{
					return (_parent.Percent - _min)/_diff;
				}
				set
				{
					value = value < 0 ? 0 : (value > 1 ? 1 : value);
					_parent.Percent = _min + value*_diff;
				}
			}

			public string Message
			{
				get { return _parent.Message; }
				set { _parent.Message = value; }
			}

			public IEnumerable<IProgressBarControler> Split(int amount)
			{
				if (amount < 1)
					throw new ArgumentException("Must be greater that 0", nameof(amount));

				var min = Percent;
				var diff = 1f - Percent;
				var steps = diff / amount;
				for (int i = 0; i < amount; i++)
				{
					yield return new SubBarControler(this, min + steps * i, steps);
				}
			}
		}
	}
}
