using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Skoblia_5 : Form
    {
        private string _filename = String.Empty;

        public Skoblia_5()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "5. Скобля";
        }

        private void Skoblia_5_Load(object sender, EventArgs e)
        {
            this.Text = "5. Скобля";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = File.OpenText(ofd.FileName))
                    textBox1.Text = sr.ReadToEnd();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filename == String.Empty)
            {
                MessageBox.Show("Нечего сохранять!");
                return;
            }

            using (StreamWriter sw = new StreamWriter(_filename))
                sw.WriteLine(textBox1.Text);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void цвет1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            menuStrip1.BackColor = this.BackColor;
            textBox1.BackColor = this.BackColor;
        }

        private void цвет2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;
            menuStrip1.BackColor = this.BackColor;
            textBox1.BackColor = this.BackColor;
        }

        private void цвет3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            menuStrip1.BackColor = this.BackColor;
            textBox1.BackColor = this.BackColor;
        }

        private void типШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowColor = false;
            if (fd.ShowDialog() == DialogResult.OK)
                textBox1.Font = fd.Font;
        }

        private void цветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                textBox1.ForeColor = cd.Color;
        }
    }
}
