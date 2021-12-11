using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_06
{
    public partial class TextEditor : Form
    {
        public TextEditor()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Текстовый редактор";
        }

        private void TextRedactor_Load(object sender, EventArgs e)
        {
            this.Text = "Текстовый редактор";
            textBox1.Text = String.Empty;
            button1.Text = "Загрузить";
            button2.Text = "Сохранить";

            this.Font = new Font(this.Font.FontFamily, this.Font.Size * 1.2f);
            textBox1.Font = new Font("Consolas", this.Font.Size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Все файлы (*.*)|*.*";
            ofd.FilterIndex = 0;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = File.OpenText(ofd.FileName))
                    textBox1.Text = sr.ReadToEnd();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    sw.Write(textBox1.Text);
            }
        }
    }
}
