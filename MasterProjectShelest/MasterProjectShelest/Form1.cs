using System;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Родительская форма";
            button1.Text = "Запустить";
            listBox1.FormattingEnabled = false;

            listBox1.Items.Add(new Jila_2());
            listBox1.Items.Add(new Trystan_7());
            listBox1.Items.Add(new Shelest_11());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите программу для запуска!");
                return;
            }
            if (!(listBox1.SelectedItem is Form))
                return;

            this.Hide();
            (listBox1.SelectedItem as Form).ShowDialog();
            this.Show();
        }
    }
}
