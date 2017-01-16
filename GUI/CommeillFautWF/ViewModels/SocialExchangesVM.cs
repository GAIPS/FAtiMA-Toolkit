using System;
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
using WellFormedNames;

namespace CommeillFautWF.ViewModels
{
   public class SocialExchangesVM 
    {
      
        public readonly BaseCIFForm _mainForm;
        private bool m_loading;
        public CommeillFautAsset _cifAsset => _mainForm.CurrentAsset;
        public BindingListView<SocialExchangeDTO> SocialExchanges { get; private set; }
  
     //   public Dictionary<string, InfluenceRuleDTO> InfluenceRulesDiccionary;

        
        public SocialExchangesVM(BaseCIFForm parent)
        {
            
            _mainForm = parent;
		    SocialExchanges = new BindingListView<SocialExchangeDTO>((IList)null);
      //      InfluenceRulesDiccionary = new Dictionary<string, InfluenceRuleDTO>();
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

            
            if (_cifAsset.m_SocialExchanges.Find(x=>x.ActionName.ToString() == newSocialExchange.Action) != null)
                _cifAsset.UpdateSocialExchange(newSocialExchange);

            else _cifAsset.AddExchange(newSocialExchange);

            
            
      //      SocialExchanges.DataSource = _cifAsset.m_SocialExchanges.ToList();
        //    SocialExchanges.Refresh();
            _mainForm.SetModified();
            Reload();
          
        }

 /*       public void AddOrUpdateInfluenceRule(InfluenceRuleDTO dto)
        {
            if (addedRules.Find(x => x.RuleName == dto.RuleName) != null)
            {
                addedRules.Remove(addedRules.Find(x => x.RuleName == dto.RuleName));
                addedRules.Add(dto);
            }
          else  addedRules.Add(dto);
           _mainForm.SetModified();
           this.Reload();
        }

      
*/
    }
}


