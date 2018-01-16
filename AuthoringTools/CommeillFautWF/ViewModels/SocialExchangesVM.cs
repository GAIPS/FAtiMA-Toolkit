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
   public class SocialExchangesVM : IDataGridViewController
    {
      
        public readonly BaseCIFForm _mainForm;
        private bool m_loading;
        public CommeillFautAsset _cifAsset => _mainForm.LoadedAsset;
        private Guid _currentlySelected = Guid.Empty;
        public BindingListView<SocialExchangeDTO> SocialExchanges { get; private set; }

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

            // InfluenceRulesDiccionary = new Dictionary<string, InfluenceRuleDTO>();
            m_loading = false;

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

        public void AddSocialMove(SocialExchangeDTO newSocialExchange)
        {

            
            if (_cifAsset.m_SocialExchanges.Find(x=>x.Name == newSocialExchange.Name) != null)
                _cifAsset.UpdateSocialExchange(newSocialExchange);

            else _cifAsset.AddExchange(newSocialExchange);



            _mainForm.SetModified();
            _mainForm.Refresh();

            Reload();

        }

        public object AddElement()
        {

            var dto = new SocialExchangeDTO();
            var dialog = new AddSocialExchange(this);
            dialog.ShowDialog();
            _mainForm.SetModified();
            _mainForm.Refresh();

            Reload();
            return dialog.AddedObject;

        }

        public IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit)
        {
            List<object> result = new List<object>();
            foreach (var dto in elementsToEdit.Cast<ObjectView<SocialExchangeDTO>>().Select(v => v.Object))
            {
                try
                {
                    var dialog = new AddSocialExchange(this, new SocialExchange(dto));
                    dialog.ShowDialog();
                    if (dialog.AddedObject != null)
                        result.Add(dialog.AddedObject);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return result;
        }

        public object EditElement(object elementToEdit)
        {
            var dto = (elementToEdit as ObjectView<SocialExchangeDTO>).Object;
            /*var dialog = new AddSocialExchange(dto);
            dialog.ShowDialog(_parent);*/
            //return dialog.AddedObject;
            return null;
        }


        public object DuplicateElement(object elementToDuplicate)
        {


            throw new NotImplementedException();
        }


        public uint RemoveElements(IEnumerable<object> elementsToRemove)
        {
            uint count = 0;
            foreach (var dto in elementsToRemove.Cast<ObjectView<SocialExchangeDTO>>().Select(v => v.Object))
            {
                try
                {
                    RemoveSocialExchangeByName(dto.Name.ToString());
                   
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
            }

            return count;
        }

        public void RemoveSocialExchangeById(Guid id)
        {
            var se = SocialExchanges.FirstOrDefault(a => a.Id == id);
            if (se == null)
                throw new Exception("Social Exchange not found");
            _cifAsset.RemoveSocialExchange(new SocialExchange(se));
            Reload();
        }

        public void RemoveSocialExchangeByName(string _name)
        {
            var se = _cifAsset.m_SocialExchanges.FirstOrDefault(a => a.Name.ToString() == _name);
           
            if (se == null)
                throw new Exception("Social Exchange not found");
            _cifAsset.RemoveSocialExchange(se);
            _mainForm.SetModified();
            _mainForm.Refresh();

            Reload();
        }
        private void UpdateSelected()
        {
            if (m_loading)
                return;

   
        }

        public IList GetElements()
        {
            return SocialExchanges;
        }

     
    }
}


