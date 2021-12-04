using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Этот метод выполняется при изменении значений любого из 3х полей
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            DateTime date;

            try
            {
                // попытаться прочесть дату с полей
                int day = int.Parse(textBox1.Text);
                int month = int.Parse(textBox2.Text);
                int year = int.Parse(textBox3.Text);

                date = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                // если данные некоректны, установить текущую дату
                date = DateTime.Now;
            }

            // установить новую дату
            monthCalendar1.SetDate(date);
            // вызвать обновление календаря
            monthCalendar1.Update();
        }
    }
}
