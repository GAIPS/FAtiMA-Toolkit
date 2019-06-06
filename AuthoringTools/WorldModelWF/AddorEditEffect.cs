using System;
using System.Windows.Forms;
using WellFormedNames;
using WorldModel;
using WorldModel.DTOs;

namespace WorldModelWF
{
    public partial class AddorEditEffect : Form
    {

        private WorldModelAsset _wm;

        private Name _eventTemplate;

        private EffectDTO _effectToEdit;

        private int index;

        private Name _subject;


        public AddorEditEffect(WorldModelAsset wm, Name eventTemplate , int _index, EffectDTO effectToEdit = null)
        {
            InitializeComponent();

            this._wm = wm;
            _eventTemplate = eventTemplate;

            //DefaultValues
            newValue.Value = WellFormedNames.Name.BuildName("True");
            propertyName.Value = WellFormedNames.Name.BuildName("Bel(A)");
            observerName.Value =WellFormedNames.Name.BuildName("*");

            //Restrictions
            index = _index;
            newValue.AllowUniversal = false;
            

            _subject = eventTemplate.GetNTerm(2);
            
            propertyName.AllowNil = false;
            propertyName.AllowUniversal = false;
            propertyName.AllowLiteral = false;
           
            _effectToEdit = effectToEdit;

            if (effectToEdit != null)
            {
                this.Text = "Edit Effect";
                this.addEffect.Text = "Update";

                newValue.Value = effectToEdit.NewValue;
                propertyName.Value = effectToEdit.PropertyName;
                observerName.Value = effectToEdit.ObserverAgent;
                
            }
            else
                _effectToEdit = new EffectDTO()
                {
                    NewValue = newValue.Value,
                    PropertyName = propertyName.Value,
                    ObserverAgent = observerName.Value,
                
                };
        }

        public AddorEditEffect()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void wfNameFieldBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void addEffect_Click(object sender, EventArgs e)
        {
            _effectToEdit.NewValue = newValue.Value;
            _effectToEdit.PropertyName = propertyName.Value;
            _effectToEdit.ObserverAgent = observerName.Value;

            if(index >= 0)
            _wm.EditEventEffect(_eventTemplate, _effectToEdit, index);
            else
            {
                _wm.AddActionEffect(_eventTemplate, _effectToEdit);
            }
            Close();
        }

        private void AddorEditEffect_Load(object sender, EventArgs e)
        {

        }

        private void observerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void propertyName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
