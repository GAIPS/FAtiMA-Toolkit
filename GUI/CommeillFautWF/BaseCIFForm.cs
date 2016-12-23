using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommeillFaut;
using GAIPS.AssetEditorTools;

namespace CommeillFautWF
{
    public class BaseCIFForm : BaseAssetForm<CommeillFautAsset>
    {
        protected sealed override CommeillFautAsset CreateEmptyAsset()
        {
            return new CommeillFautAsset();
        }

        protected sealed override string GetAssetFileFilters()
        {
            return "Comme ill Faut Definition File (*.cif)|*.cif";
        }
    }
}