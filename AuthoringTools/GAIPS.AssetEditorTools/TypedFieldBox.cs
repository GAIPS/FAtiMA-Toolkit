using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GAIPS.AssetEditorTools
{
	public abstract class TypedFieldBox<T> : TextBox
	{
		private T _value;
		private bool _inEditMode = false;
		private Color _normalColor;

		public event EventHandler OnValueChanged;

		protected ITypeConversionProvider<T> ConversionProvider { get; private set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public T Value
		{
			get { return _value; }
			set
			{
				if(Equals(value,_value))
					return;

				_value = value;
				UpdateText();
				DispatchOnValueChanged(new EventArgs());
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string Text {
			get { throw new InvalidOperationException("Invalid Access to the Property 'Text'");}
			set { throw new InvalidOperationException("Invalid Access to the Property 'Text'");}
		}

		protected TypedFieldBox(T defaultValue, ITypeConversionProvider<T> conversionProvider)
		{
			_value = defaultValue;
			ConversionProvider = conversionProvider;
			UpdateText();

			this.GotFocus += OnFocus;
			this.LostFocus += OnFocusLost;
		}

		private void OnFocus(object sender, EventArgs e)
		{
			if(_inEditMode)
				return;

			_inEditMode = true;
			_normalColor = BackColor;
			this.TextChanged += OnTextChanged;
			KeyPress += OnKeyPress;
		}

		private void OnFocusLost(object sender, EventArgs e)
		{
			if(!_inEditMode)
				return;

			_inEditMode = false;
			this.TextChanged -= OnTextChanged;
			KeyPress -= OnKeyPress;
			BackColor = _normalColor;

			//Update value, or revert text to current value's string
			T newValue;
			if (ConversionProvider.TryToParseType(base.Text, out newValue))
			{
                if (ValidateValue(newValue))
                {
                    _value = newValue;
                    DispatchOnValueChanged(new EventArgs());
                }
			}

			UpdateText();
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			/*if (e.KeyChar == (char) Keys.Return)
				this.FindForm().ActiveControl=null;*/
		}

		private void OnTextChanged(object sender, EventArgs e)
		{
			T value;
			var valid = ConversionProvider.TryToParseType(base.Text, out value);
			if (valid)
            {
                valid = ValidateValue(value);
                if (valid) _value = value;
            }
				

			BackColor = valid ? _normalColor : Color.Red;
		}

		private void UpdateText()
		{
			base.Text = ConversionProvider.ToString(_value);
			Refresh();
		}

		private void DispatchOnValueChanged(EventArgs args)
		{
			Invalidate();
			OnValueChanged?.Invoke(this,args);
		}

		protected virtual bool ValidateValue(T value)
		{
			return true;
		}
	}
}