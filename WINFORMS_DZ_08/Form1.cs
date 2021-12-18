using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_08
{
    public partial class Form1 : Form
    {
        private ChessBoard board;

        public Form1()
        {
            InitializeComponent();
            InitializePaintingEngine();

            board = new ChessBoard();
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ДЗ №8, Шелест";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            board.OnMouseDown(sender, e);
            panel1.Refresh();
        }
    }
}
