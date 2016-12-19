using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommeillFaut;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;
using CommeillFautWF;

namespace CommeillFautWF.ViewModels
{
   public class SocialExchangesVM 
    {
      
        private readonly BaseCIFForm _mainForm;
        private bool m_loading;
        public CommeillFautAsset _cifAsset => _mainForm.CurrentAsset;
        public BindingListView<SocialExchangeDTO> SocialExchanges { get; private set; }
        public Dictionary<string, InfluenceRuleDTO> _rules;

        public SocialExchangesVM(BaseCIFForm parent)
        {
            
            _mainForm = parent;
		this.SocialExchanges = new BindingListView<SocialExchangeDTO>((IList)null);
            _rules = new Dictionary<string, InfluenceRuleDTO>();
            m_loading = false;
            
        }

     
        public void Reload()
        {
            m_loading = true;

            SocialExchanges.Refresh();

            m_loading = false;
        }

        public void AddSocialMove(SocialExchangeDTO newSocialExchange)
        {
            if (_rules.Count > 0)
            {
                newSocialExchange.InfluenceRules = _rules.Values.ToList();
                MessageBox.Show(" yo " + _rules.Values.ToList()[0].RuleName);
            }

            _cifAsset.AddExchange(newSocialExchange);
      //      SocialExchanges.DataSource = _cifAsset.m_SocialExchanges.ToList();
        //    SocialExchanges.Refresh();
            _mainForm.SetModified();
           
        }

        public void AddInfluenceRule(InfluenceRuleDTO _dto)
        {
           _rules.Add(_dto.RuleName, _dto);
        //    _cifAsset.UpdateSocialExchange(_dto);
            _mainForm.SetModified();
        }
 

    }
}


/* using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using SocialImportance.DTOs;

namespace SocialImportanceWF.ViewModels
{
	public class ClaimsVM: IDataGridViewController
	{
		

		#endregion
	}
}
*/