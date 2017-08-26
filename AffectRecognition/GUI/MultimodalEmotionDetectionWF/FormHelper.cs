using System.Windows.Forms;

namespace MultimodalEmotionDetectionWF
{
    public static class FormHelper
    {
        public static void ShowFormInContainerControl(Control ctl, Form frm)
        {
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            ctl.Controls.Add(frm);
        }
    }
}
