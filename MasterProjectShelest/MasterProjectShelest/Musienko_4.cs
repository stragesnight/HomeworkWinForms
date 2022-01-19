using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterProjectShelest
{
    public partial class Musienko_4 : Form
    {
        private Image _img;
        private Graphics _g;
        private Pen _pen = new Pen(Color.Black, 1);
        private bool _isDrawing = false;
        private Point _prevMousePos = Point.Empty;

        public Musienko_4()
        {
            InitializeComponent();
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);
            panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        public override string ToString()
        {
            return "4. Мусиенко";
        }

        private void Musienko_4_Load(object sender, EventArgs e)
        {
            this.Text = "4. Мусиенко";
            _img = new Bitmap(panel1.Width, panel1.Height);
            _g = Graphics.FromImage(_img);
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 100;
        }

        private void UpdatePanelGraphics()
        {
            panel1.CreateGraphics().DrawImage(_img, Point.Empty);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _isDrawing = true;
            _prevMousePos = e.Location;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _isDrawing = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing)
                return;

            _g.DrawLine(_pen, _prevMousePos, e.Location);
            UpdatePanelGraphics();
            _prevMousePos = e.Location;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            UpdatePanelGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                _pen.Color = cd.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _g.FillRectangle(Brushes.White, new Rectangle(Point.Empty, panel1.Size));
            UpdatePanelGraphics();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _pen.Width = (float)numericUpDown1.Value;
        }
    }
}
