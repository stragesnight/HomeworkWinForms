using System;
using System.Drawing;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Shelest_11 : Form
    {
        private Size _initImageSize = new Size(0, 0);

        public Shelest_11()
        {
            InitializeComponent();
            pictureBox1.SizeChanged += pictureBox1_SizeChanged;
        }

        public override string ToString()
        {
            return "11. Шелест";
        }

        private void Shelest_11_Load(object sender, EventArgs e)
        {
            this.Text = "11. Шелест";
            pictureBox1.Visible = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        { 
            this.ClientSize = new Size(pictureBox1.Width, pictureBox1.Height + 26);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы изображений (*.png, *.bmp; *.jpeg)|*.png;*.bmp;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                pictureBox1.Size = pictureBox1.Image.Size;
                pictureBox1.Visible = true;
                _initImageSize = pictureBox1.Size;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            _initImageSize = new Size(0, 0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void zoomIn25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newWidth = (int)(pictureBox1.Size.Width * 1.25f);
            int newHeight = (int)(pictureBox1.Size.Height * 1.25f);
            pictureBox1.Size = new Size(newWidth, newHeight);
        }

        private void zoomOut25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newWidth = (int)(pictureBox1.Size.Width * 0.75f);
            int newHeight = (int)(pictureBox1.Size.Height * 0.75f);
            pictureBox1.Size = new Size(newWidth, newHeight);
        }

        private void resetSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_initImageSize.Width != 0)
                pictureBox1.Size = _initImageSize;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа для просмотра изображений.\nАвтор: Шелест Александр, 2022", "О программе");
        }
    }
}
