using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;


namespace CommeillFautWF.ViewModels
{
	public class InfluenceRuleVM
	{
		private BaseCIFForm _parent;
		private Guid _currentlySelected = Guid.Empty;
		private bool m_loading = false;

		public List<InfluenceRuleDTO> RuleList { get; }
		

		

		public InfluenceRuleVM(BaseCIFForm parent)
		{
			_parent = parent;
			RuleList = new List<InfluenceRuleDTO>();
			
		}
        public InfluenceRuleVM(SocialExchangesVM _current)
        {
            _parent = _current._mainForm;
           RuleList = new List<InfluenceRuleDTO>();
        }



		public void Reload()
		{
			m_loading = true;

		//	RuleList.DataSource = _parent.CurrentAsset.GetAttributionRules().ToList();
			RuleList.Clear();

	//		ConditionSetView.SetData(null);

			m_loading = false;
		}

		public void AddOrUpdateInfluenceRule(InfluenceRuleDTO dto)
		{

		    if (RuleList.Find(x => x.RuleName == dto.RuleName) != null)
		    {
		        RuleList.Remove(RuleList.Find(x => x.RuleName == dto.RuleName));
                RuleList.Add(dto);

		    }
		    else RuleList.Add(dto);
            //      SocialExchanges.DataSource = _cifAsset.m_SocialExchanges.ToList();
            //    SocialExchanges.Refresh();
            _parent.SetModified();
        }

	
	}
}