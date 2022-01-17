using System;
using System.Drawing;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Jila_2 : Form
    {
        public Jila_2()
        {
            InitializeComponent();
        }

        private void Jila_2_Load(object sender, EventArgs e)
        {
            this.Text = "2. Жила";
            label1.Text = "Расчет площади и периметра фигуры";
            label4.Text = "Выберите фигуру:";
            button1.Text = "Рассчитать";
            label6.Text = "Результаты";
            label3.Text = "S = ";
            label5.Text = "P = ";
            label7.Text = "см2";
            label8.Text = "см";
            label9.Visible = false;
            textBox4.Visible = false;

            listBox1.Items.Add(new Circle());
            listBox1.Items.Add(new Square());
            listBox1.Items.Add(new Triangle());
        }

        public override string ToString()
        {
            return "2. Жила";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Figure fig = listBox1.SelectedItem as Figure;
            fig.Draw(panel1);

            label2.Text = fig.FirstParam() + ": ";
            label9.Visible = false;
            textBox4.Visible = false;

            if (fig.GetNParams() == 2)
            {
                label9.Visible = true;
                textBox4.Visible = true;
                label9.Text = fig.SecondParam() + ": ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float v1 = 0;
            float v2 = 0;

            if (!float.TryParse(textBox1.Text, out v1))
            {
                MessageBox.Show("Некоррекнтые данные");
                return;
            }

            if (textBox4.Visible && !float.TryParse(textBox4.Text, out v2))
            {
                MessageBox.Show("Некоррекнтые данные");
                return;
            }

            Figure fig = listBox1.SelectedItem as Figure;
            textBox2.Text = String.Format("{0:N2}", fig.GetArea(v1, v2));
            textBox3.Text = String.Format("{0:N2}", fig.GetPerimeter(v1, v2));
        }
    }

    public abstract class Figure
    {
        protected static Brush brush = Brushes.LightBlue;
        protected static Pen pen = new Pen(Color.Black, 2);

        public abstract void Draw(Control parent);
        public abstract int GetNParams();
        public abstract string FirstParam();
        public abstract string SecondParam();

        public abstract float GetPerimeter(float v1, float v2);
        public abstract float GetArea(float v1, float v2);
    }

    public class Circle : Figure
    {
        public override string ToString() => "Круг";

        public override void Draw(Control parent)
        {
            Graphics g = parent.CreateGraphics();
            Size s = parent.Size;
            Rectangle rect = new Rectangle(Point.Empty, s);

            g.FillRectangle(new SolidBrush(parent.BackColor), rect);
            g.DrawEllipse(pen, rect);
            g.FillEllipse(brush, rect);
        }

        public override int GetNParams() => 1;
        public override string FirstParam() => "Радиус";
        public override string SecondParam() => String.Empty;

        public override float GetPerimeter(float v1, float v2) => v1 * 2 * (float)Math.PI;
        public override float GetArea(float v1, float v2) => (float)Math.PI * v1 * v1;
    }

    public class Square : Figure
    {
        public override string ToString() => "Квадрат";

        public override void Draw(Control parent)
        {
            Graphics g = parent.CreateGraphics();
            Size s = parent.Size;
            Rectangle rect = new Rectangle(Point.Empty, s);

            g.FillRectangle(new SolidBrush(parent.BackColor), rect);
            g.DrawRectangle(pen, rect);
            g.FillRectangle(brush, rect);
        }

        public override int GetNParams() => 1;
        public override string FirstParam() => "Сторона";
        public override string SecondParam() => String.Empty;

        public override float GetPerimeter(float v1, float v2) => v1 * 4;
        public override float GetArea(float v1, float v2) => v1 * v1;
    }

    public class Triangle : Figure
    {
        public override string ToString() => "Треугольник";

        public override void Draw(Control parent)
        {
            Graphics g = parent.CreateGraphics();
            Size s = parent.Size;
            Rectangle rect = new Rectangle(Point.Empty, s);
            Point[] points = {
                new Point(0, 0),
                new Point(0, s.Height),
                new Point(s)
            };

            g.FillRectangle(new SolidBrush(parent.BackColor), rect);
            g.DrawPolygon(pen, points);
            g.FillPolygon(brush, points);
        }

        public override int GetNParams() => 2;
        public override string FirstParam() => "1-й катет";
        public override string SecondParam() => "2-й катет";

        public override float GetPerimeter(float v1, float v2) => v1 + v2 + (float)Math.Sqrt((v1 * v1) + (v2 + v2));
        public override float GetArea(float v1, float v2) => (v1 * v2) / 2;
    }
}

