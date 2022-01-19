using System;
using System.IO;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Trystan_7 : Form
    {
        private string _filename = String.Empty;
        private string _buffer = String.Empty;

        public Trystan_7()
        {
            InitializeComponent();
        }

        private void Trystan_7_Load(object sender, EventArgs e)
        {
            this.Text = "7. Тристан";
        }

        public override string ToString()
        {
            return "7. Тристан";
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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _buffer = textBox1.SelectedText;
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_buffer != String.Empty)
                textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, _buffer);
        }

        private void выделитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength < 1)
            {
                MessageBox.Show("Выберите текст для удаления");
                return;
            }

            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
        }

        private void шрифтToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                textBox1.Font = fd.Font;
        }

        private void цветШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                textBox1.ForeColor = cd.Color;
        }

        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                textBox1.BackColor = cd.Color;
        }
    }
}

