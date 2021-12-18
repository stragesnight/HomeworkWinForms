using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WINFORMS_DZ_08
{
    public partial class Form1 : Form
    {
        private void InitializePaintingEngine()
        {
            this.ClientSize = ChessConstants.CanvasSize;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            panel1.Location = new Point(0, 0);
            panel1.Size = ChessConstants.CanvasSize;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(SystemColors.Control);
            board.Draw(g);
            g.Dispose();
        }
    }
}
