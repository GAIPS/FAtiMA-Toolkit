using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntegratedAuthoringTool;
using WorldModel;
using WorldModel.DTOs;

namespace WorldModelWF
{
    public partial class AddorEditEffect : Form
    {

        private WorldModelAsset _wm;

        private EffectDTO _effectToEdit;


        public AddorEditEffect(WorldModelAsset wm, EffectDTO effectToEdit = null)
        {
            InitializeComponent();

            this._wm = wm;

            //DefaultValues
            newValue.Value = WellFormedNames.Name.BuildName("True");
            propertyName.Value = WellFormedNames.Name.BuildName("Bel(A)");
            responsibleAgent.Value = WellFormedNames.Name.BuildName("SELF");
            observerName.Value =WellFormedNames.Name.BuildName("*");

            //Restrictions

        
            newValue.AllowUniversal = false;
          

            
            propertyName.AllowNil = false;
            propertyName.AllowUniversal = false;
           


            _effectToEdit = effectToEdit;

            if (effectToEdit != null)
            {
                this.Text = "Edit Belief";
                this.addEffect.Text = "Update";

                newValue.Value =effectToEdit.NewValue;
                propertyName.Value = effectToEdit.PropertyName;
                responsibleAgent.Value = effectToEdit.ResponsibleAgent;
                observerName.Value = effectToEdit.ObserverAgent;
            }
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
            _effectToEdit.NewValue = newValue.Value;
            _effectToEdit.PropertyName = propertyName.Value;


            Close();
        }
    }
}
