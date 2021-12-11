using System;
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
    public partial class ParentForm : Form
    {
        public ParentForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ДЗ №6, Шелест (Родительская форма)";
            textBox1.Text = String.Empty;
            button1.Text = "Запустить";
            button2.Text = "Выйти";

            listBox1.FormattingEnabled = false;

            this.Font = new Font(this.Font.FontFamily, this.Font.Size * 1.2f);

            listBox1.Items.Add(new Calculator());
            listBox1.Items.Add(new TextEditor());
            listBox1.Items.Add(new ProductList());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Нету выбранной программы");
                return;
            }

            this.Hide();
            (listBox1.SelectedItem as Form).ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }
    }
}
