using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WINFORMS_DZ_09
{
    public partial class Form1 : Form
    {
        // Событие перемещения указателя мыши
        public delegate void MouseHandler(Point point);
        public event MouseHandler MouseMoved;
        // Событие изменения состояния указателя мыши
        public delegate void MousePressHandler(Point point, bool pressed);
        public event MousePressHandler MouseStateChanged;

        // Событие отрисовки графики
        public delegate void PaintHandler();
        public event PaintHandler PaintEvent;

        // Событие изменения инструмента/цвета рисования
        public delegate void ListBoxHandler(ListBox listBox);
        public event ListBoxHandler InstrumentIndexChanged;
        public event ListBoxHandler ColorIndexChanged;

        // Событие изменения толщины ручки
        public delegate void PenWeightHandler(int value);
        public event PenWeightHandler PenWeightChanged;

        private PaintingEngine _paintingEngine;

        public Form1()
        {
            InitializeComponent();
            // инициализация меню сверху
            InitializeMenuStrip();

            // инициализация движка отрисовки
            _paintingEngine = new PaintingEngine(this, panel1);

            // подписка на события
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseUp += panel1_MouseUp;
            panel1.MouseMove += panel1_MouseMove;
            panel1.Paint += panel1_Paint;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox2.SelectedIndexChanged += ListBox2_SelectedIndexChanged;
            trackBar1.ValueChanged += trackBar1_ValueChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // задание изначального состояния программы

            this.Text = "ДЗ №9, Шелест";
            label1.Text = "Инструменты";
            label2.Text = "Цвета";
            label3.Text = "Толщина ручки";

            trackBar1.Minimum = 1;
            trackBar1.Maximum = 100;
            trackBar1.Value = 2;

            listBox1.Items.Clear();
            listBox1.Items.AddRange(
                Enum.GetNames(typeof(PaintingEngine.DrawingInstrument))
            );
            listBox1.SelectedIndex = 0;

            listBox2.Items.Clear();
            List<KnownColor> colors = new List<KnownColor>(
                (IEnumerable<KnownColor>)Enum.GetValues(typeof(KnownColor))
            );
            for (int i = colors.IndexOf(KnownColor.Aqua); i < colors.Count; ++i)
                listBox2.Items.Add(colors[i].ToString());
            listBox2.SelectedIndex = 0;

            panel1.AutoScroll = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStateChanged?.Invoke(e.Location, true);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseStateChanged?.Invoke(e.Location, false);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // при перемещении указателя стоит запомнить его новую позицию
            // а также отрисовать графику
            MouseMoved?.Invoke(e.Location);
            PaintEvent?.Invoke();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // перенаправить функцию рисования на нашу собственную
            PaintEvent?.Invoke();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // показать ползунок и текст о толщине ручки
            // если выбрана ручка
            trackBar1.Visible = listBox1.SelectedIndex == 0;
            label3.Visible = listBox1.SelectedIndex == 0;

            InstrumentIndexChanged?.Invoke(listBox1);
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorIndexChanged?.Invoke(listBox2);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "Толщина ручки: " + trackBar1.Value.ToString();
            PenWeightChanged?.Invoke(trackBar1.Value);
        }
    }
}
