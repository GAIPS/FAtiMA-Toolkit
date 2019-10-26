using System.Windows.Forms;

namespace GAIPS.AssetEditorTools
{
    public static class FormHelper
    {
        public static void ShowFormInContainerControl(Control ctl, Form frm)
        {
            if(frm.MainMenuStrip != null)
                frm.MainMenuStrip.Visible = false;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            ctl.Controls.Clear();
            ctl.Controls.Add(frm);
        }
    }
}
