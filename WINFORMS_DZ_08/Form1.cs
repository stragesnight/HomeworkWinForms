using System;
using System.Drawing;
using System.Windows.Forms;

namespace WINFORMS_DZ_08
{
    public partial class Form1 : Form
    {
        private ChessBoard board;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Шахматы работают - могут сыграть 2 человека за одним компютером\n\n"
                + "Левая кнопка мыши - выбрать/переместить фигуру\n"
                + "Правая кнопка мыши - контекстное меню\n",
                "ДЗ №8, Шелест"
            );

            this.Text = "ДЗ №8, Шелест";
            this.ClientSize = ChessConstants.CanvasSize;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            ContextMenu contextMenu = new ContextMenu();

            panel1.Location = new Point(0, 0);
            panel1.Size = ChessConstants.CanvasSize;
            panel1.ContextMenu = contextMenu;
            panel1.Paint += new PaintEventHandler(panel1_Paint);

            board = new ChessBoard(contextMenu);
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            contextMenu.Popup += new EventHandler(board.OnShowPopup);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(SystemColors.Control);
            board.Draw(g);
            g.Dispose();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            board.OnMouseDown(sender, e);
            panel1.Refresh();
        }
    }
}
