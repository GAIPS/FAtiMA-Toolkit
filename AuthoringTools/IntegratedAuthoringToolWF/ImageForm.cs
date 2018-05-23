using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF
{
    public partial class ImageForm : Form
    {
        public ImageForm( Bitmap b)
        {
            InitializeComponent();

       
            panel1.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = b;

           

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            pictureBox1.Image.Save(@"FilePath", ImageFormat.Png);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image File|*.png";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            var fileName = new FileInfo(sfd.FileName);
            pictureBox1.Image.Save(fileName.ToString());
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
