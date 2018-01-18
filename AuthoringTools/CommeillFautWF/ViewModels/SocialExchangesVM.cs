using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommeillFaut;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;

using WellFormedNames;

namespace CommeillFautWF.ViewModels
{
   public class SocialExchangesVM : IDataGridViewController
   {
        public readonly BaseCIFForm _mainForm;
        private bool m_loading;

        public CommeillFautAsset _cifAsset => _mainForm.LoadedAsset;
        private Guid _currentlySelected = Guid.Empty;

        public BindingListView<SocialExchangeDTO> SocialExchanges { get; private set; }

        public ConditionSetView ConditionSetView { get; }

        public Guid Selection
        {
            get { return _currentlySelected; }
            set
            {
                if (_currentlySelected == value)
                    return;

                _currentlySelected = value;
                UpdateSelected();
            }
        }

        public SocialExchangeDTO CurrentlySelectedSE
        {
            get
            {
                if (_currentlySelected == Guid.Empty)
                    return null;
                var currentSe = SocialExchanges.FirstOrDefault(se => se.Id == _currentlySelected);
                
                return currentSe;
            }
        }
        public SocialExchangesVM(BaseCIFForm parent)
        {
            _mainForm = parent;
            var _aux = new List<SocialExchangeDTO>();
            if(_cifAsset != null)
            {
                foreach (var s in _cifAsset.m_SocialExchanges)
                {
                    _aux.Add(s.ToDTO());
                }
            }
            SocialExchanges = new BindingListView<SocialExchangeDTO>(_aux);

            ConditionSetView = new ConditionSetView();
            ConditionSetView.OnDataChanged += ConditionSetView_OnDataChanged;
            
            m_loading = false;
        }


        private void ConditionSetView_OnDataChanged()
        {
            if (m_loading)
                return;

            var se = CurrentlySelectedSE;

            if (se == null)
                return;

            se.Conditions = ConditionSetView.GetData();
            _mainForm.LoadedAsset.AddOrUpdateExchange(se);
            _mainForm.SetModified();
        }

        public void Reload()
        {
            m_loading = true;

        
            var _aux = new List<SocialExchangeDTO>();
            foreach (var s in _cifAsset.m_SocialExchanges)
                _aux.Add(s.ToDTO());
            SocialExchanges = new BindingListView<SocialExchangeDTO>(_aux);
           
            SocialExchanges.Refresh();
            m_loading = false;
        }


        public void AddOrUpdateSocialExchange(SocialExchangeDTO dto)
        {
            var resultId = _cifAsset.AddOrUpdateExchange(dto);
            _mainForm.SetModified();
            _mainForm.Refresh();
            Reload();
        }

        public object AddElement()
        {
            var dto = new SocialExchangeDTO()
            {
                Description = "-",
                Name = Name.BuildName("SE1"),
            };
            var dialog = new AddSocialExchange(this, dto);
            dialog.ShowDialog(_mainForm);
            return dialog.AddedObject;
        }

        public object EditElement(object elementToEdit)
        {
            var dto = (elementToEdit as ObjectView<SocialExchangeDTO>).Object;
            var dialog = new AddSocialExchange(this, dto);
            dialog.ShowDialog(_mainForm);
            return dialog.AddedObject;
        }


        public object DuplicateElement(object elementToDuplicate)
        {
            var dto = (elementToDuplicate as ObjectView<SocialExchangeDTO>).Object;
            dto.Id = Guid.Empty;
            var resultId = _cifAsset.AddOrUpdateExchange(dto);
            return _cifAsset.GetSocialExchange(resultId);
        }


        public uint RemoveElements(IEnumerable<object> elementsToRemove)
        {
            uint count = 0;
            foreach (var dto in elementsToRemove.Cast<ObjectView<SocialExchangeDTO>>().Select(v => v.Object))
            {
                try
                {
                    _cifAsset.RemoveSocialExchange(dto.Id);
                    count++;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (count > 0)
            {
                Reload();
                _mainForm.SetModified();
                _mainForm.Refresh();
            }
            return count;
        }

        private void UpdateSelected()
        {
            if (m_loading)
                return;

            var rule = CurrentlySelectedSE;

            if (CurrentlySelectedSE == null)
            {
                ConditionSetView.SetData(null);
                return;
            }
            m_loading = true;
            ConditionSetView.SetData(rule.Conditions);
            m_loading = false;
        }

        public IList GetElements()
        {
            return SocialExchanges;
        }
    }
}


