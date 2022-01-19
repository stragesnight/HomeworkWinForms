using System;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Chernishov_10 : Form
    {
        public Chernishov_10()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "10. Чернышов";
        }

        private void Chernishov_10_Load(object sender, EventArgs e)
        {
            this.Text = "10. Чернышов";
            this.button1.Click += new EventHandler(Winter);
            this.button2.Click += new EventHandler(Spring);
            this.button3.Click += new EventHandler(Summer);
            this.button4.Click += new EventHandler(Autumn);
        }

        private void Winter(object sender, EventArgs e)
        {
            string[] holidays = { 
                "День св. Николая", 
                "Новый Год", 
                "Рождество",
                "День св. Валентина"
            };
            listBox1.Items.Clear();
            listBox1.Items.AddRange(holidays);
            pictureBox1.Image = Properties.Resources.Зима;
        }

        private void Spring(object sender, EventArgs e)
        {
            string[] holidays = { 
                "Масленица",
                "Пасха",
                "День вышиванки"
            };
            listBox1.Items.Clear();
            listBox1.Items.AddRange(holidays);
            pictureBox1.Image = Properties.Resources.Весна;

        }

        private void Summer(object sender, EventArgs e)
        {
            string[] holidays = { 
                "Яблочный спас",
                "Ивана Купала",
                "День Конституции Украины",
            };
            listBox1.Items.Clear();
            listBox1.Items.AddRange(holidays);
            pictureBox1.Image = Properties.Resources.Лето;

        }

        private void Autumn(object sender, EventArgs e)
        {
            string[] holidays = { 
                "День программиста", 
                "Хэллоуин",
                "Ханука"
            };
            listBox1.Items.Clear();
            listBox1.Items.AddRange(holidays);
            pictureBox1.Image = Properties.Resources.Осень;
        }
    }
}
