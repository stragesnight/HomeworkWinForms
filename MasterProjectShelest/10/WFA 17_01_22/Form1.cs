using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_17_01_22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button1.Click += new System.EventHandler(Winter);
            this.button2.Click += new System.EventHandler(Spring);
            this.button3.Click += new System.EventHandler(Summer);
            this.button4.Click += new System.EventHandler(Autumn);
        }

        private void Winter(object sender, EventArgs e)
        {
            string[] Win = { "" , "kdgndnrl" };
            listBox1.Items.Clear();
            pictureBox1.Image = Properties.Resources.Зима;
            listBox1.Items.AddRange(Win);
        }

        private void Spring(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            pictureBox1.Image = Properties.Resources.Весна;

        }

        private void Summer(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            pictureBox1.Image = Properties.Resources.Лето;

        }

        private void Autumn(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            pictureBox1.Image = Properties.Resources.Осень;

        }
    }
}
