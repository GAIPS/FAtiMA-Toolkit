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
   public class TriggerRulesVM 
    {
      
        public readonly BaseCIFForm _mainForm;
        private bool m_loading;
        public CommeillFautAsset _cifAsset => _mainForm.LoadedAsset;
        public TriggerRulesDTO _TriggerRulesDtos;
  
     //   public Dictionary<string, InfluenceRuleDTO> InfluenceRulesDiccionary;

        
        public TriggerRulesVM(BaseCIFForm parent)
        {
            
            _mainForm = parent;
		    _TriggerRulesDtos = new TriggerRulesDTO();
            m_loading = false;
         
        }

     
        public void Reload()
        {
            m_loading = true;

        //    _TriggerRulesDtos
     
            m_loading = false;
        }

        public void AddTriggerRule(InfluenceRuleDTO newRule, string cond)
        {


            _cifAsset._TriggerRules.UpdateTriggerRule(newRule, cond);

            _mainForm.SetModified();
        }



   
      
    }
}


