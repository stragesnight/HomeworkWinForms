using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_01
{
    public partial class Form1 : Form
    {
        private bool _enabled = false;

        public Form1()
        {
            InitializeComponent();
            UpdateComponents();
        }

        private void UpdateComponents()
        {
            label9.Visible = _enabled;
            label10.Visible = _enabled;
            label11.Visible = _enabled;
            label12.Visible = _enabled;
            label13.Visible = _enabled;
            label14.Visible = _enabled;
            dateTimePicker1.Visible = _enabled;
            radioButton1.Visible = _enabled;
            radioButton2.Visible = _enabled;
            button1.Text = _enabled ? "СКРЫТЬ РЕЗУЛЬТАТЫ"
                           : "ПОКАЗАТЬ РЕЗУЛЬАТЫ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // "перевернуть" видимость элементов
            _enabled = !_enabled;
            // обновить фактическое состояние объектов
            UpdateComponents();
        }

        // Предотвратить изменение состояния элементов формы пользователем
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Control).Enabled = false;
        }

        private void control_MouseLeave(object sender, EventArgs e)
        {
            (sender as Control).Enabled = true;
        }
    }
}
