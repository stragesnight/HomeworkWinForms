using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Project-2 Zhila";
            label1.Text = "Расчет площади и периметра фигуры";
            label4.Text = "Выберите фигуру:";
            button1.Text = "Рассчитать";
            label6.Text = "Результаты";
            label3.Text = "S = ";
            label5.Text = "P = ";
            label7.Text = "см2";
            label8.Text = "см";
            label9.Text = "";
        }
    }
}
