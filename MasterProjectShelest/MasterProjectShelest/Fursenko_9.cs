using System;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Fursenko_9 : Form
    {
        private Random _r;

        public Fursenko_9()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "9. Фурсенко";
        }

        private void Fursenko_9_Load(object sender, EventArgs e)
        {
            this.Text = "9. Фурсенко";
            _r = new Random();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == String.Empty)
            {
                MessageBox.Show("Поле пустое, добавлять нечего.");
                return;
            }

            listBox1.Items.Add(textBox4.Text);
            textBox4.Text = String.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count < 1)
            {
                MessageBox.Show("Список пуст, выбирать нечего.");
                return;
            }

            int index = _r.Next(0, listBox1.Items.Count);
            textBox5.Text = (string)listBox1.Items[index];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool success = int.TryParse(textBox1.Text, out int from);
            success &= int.TryParse(textBox2.Text, out int to);
            success &= from <= to;

            if (!success)
            {
                MessageBox.Show("Введены некоректные данные.");
                return;
            }

            textBox3.Text = _r.Next(from, to).ToString();
        }
    }
}
