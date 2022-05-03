using Equin.ApplicationFramework;
using KnowledgeBase.DTOs;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF.Metabeliefs
{
    public partial class MetaBeliefForm : Form
    {
        private RolePlayCharacterAsset rpc;

        public MetaBeliefForm(RolePlayCharacterAsset _rpc)
        {
            InitializeComponent();
            this.rpc = _rpc;
            dataGridViewAgentInspector.DataSource = new BindingListView<DynamicPropertyDTO>(_rpc.GetAllDynamicProperties().ToList());
        }
    }
}
