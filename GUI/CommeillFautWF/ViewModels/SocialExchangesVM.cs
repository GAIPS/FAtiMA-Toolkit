using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;

namespace CommeillFautWF.ViewModels
{
   public class SocialExchangesVM 
    {
        private BaseCIFForm _parent;
        private bool m_loading;


     
        public void Reload()
        {
            m_loading = true;

            

            m_loading = false;
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