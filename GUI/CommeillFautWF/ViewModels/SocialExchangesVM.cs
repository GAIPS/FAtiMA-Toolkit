using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
      
        public readonly BaseCIFForm _mainForm;
        private bool m_loading;
        public CommeillFautAsset _cifAsset => _mainForm.CurrentAsset;
        public BindingListView<SocialExchangeDTO> SocialExchanges { get; private set; }
        public List<InfluenceRuleDTO> addedRules;

        
        public SocialExchangesVM(BaseCIFForm parent)
        {
            
            _mainForm = parent;
		this.SocialExchanges = new BindingListView<SocialExchangeDTO>((IList)null);
   //         _rules = new Dictionary<string, InfluenceRuleDTO>();
            m_loading = false;
            
        }

     
        public void Reload()
        {
            m_loading = true;

            SocialExchanges.Refresh();
 //           _rules.Clear();
            m_loading = false;
        }

        public void AddSocialMove(SocialExchangeDTO newSocialExchange)
        {
           

            if (_cifAsset.m_SocialExchanges.Find(x=>x.ActionName.ToString() == newSocialExchange.Action) != null)
                _cifAsset.UpdateSocialExchange(newSocialExchange);
            else _cifAsset.AddExchange(newSocialExchange);

            
      //      SocialExchanges.DataSource = _cifAsset.m_SocialExchanges.ToList();
        //    SocialExchanges.Refresh();
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