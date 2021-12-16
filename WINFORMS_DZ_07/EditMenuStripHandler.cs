using System;
using System.Windows.Forms;

namespace WINFORMS_DZ_07
{
    public partial class MainForm : Form
    {
        private string _prevTextBuffer = String.Empty;
        private string _copyBuffer = String.Empty;

        private void InitializeEditMenuStrip()
        {
            var editMenu = (ToolStripMenuItem)menuStrip1.Items.Add("Edit");

            var editUndo = (ToolStripMenuItem)editMenu.DropDownItems.Add("Undo");
            editUndo.ShortcutKeys = Keys.Control | Keys.Z;
            editUndo.ShowShortcutKeys = true;
            editUndo.Click += new EventHandler(editUndo_Click);

            var editRedo = (ToolStripMenuItem)editMenu.DropDownItems.Add("Redo");
            editRedo.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
            editRedo.ShowShortcutKeys = true;
            editRedo.Click += new EventHandler(editRedo_Click);

            editMenu.DropDownItems.Add(new ToolStripSeparator());

            var editCut = (ToolStripMenuItem)editMenu.DropDownItems.Add("Cut");
            editCut.ShortcutKeys = Keys.Control | Keys.X;
            editCut.ShowShortcutKeys = true;
            editCut.Click += new EventHandler(editCut_Click);

            var editCopy = (ToolStripMenuItem)editMenu.DropDownItems.Add("Copy");
            editCopy.ShortcutKeys = Keys.Control | Keys.C;
            editCopy.ShowShortcutKeys = true;
            editCopy.Click += new EventHandler(editCopy_Click);

            var editPaste = (ToolStripMenuItem)editMenu.DropDownItems.Add("Paste");
            editPaste.ShortcutKeys = Keys.Control | Keys.V;
            editPaste.ShowShortcutKeys = true;
            editPaste.Click += new EventHandler(editPaste_Click);

            var editDuplicate = (ToolStripMenuItem)editMenu.DropDownItems.Add("Duplicate");
            editDuplicate.ShortcutKeys = Keys.Control | Keys.D;
            editDuplicate.ShowShortcutKeys = true;
            editDuplicate.Click += new EventHandler(editDuplicate_Click);

            editMenu.DropDownItems.Add(new ToolStripSeparator());

            var editSelectAll = (ToolStripMenuItem)editMenu.DropDownItems.Add("Select All");
            editSelectAll.ShortcutKeys = Keys.Control | Keys.A;
            editSelectAll.ShowShortcutKeys = true;
            editSelectAll.Click += new EventHandler(editSelectAll_Click);
        }

        private void editUndo_Click(object sender, EventArgs e)
        {
            _prevTextBuffer = textBox1.Text;
            textBox1.Undo();
            textBox1.ScrollToCaret();
        }

        private void editRedo_Click(object sender, EventArgs e)
        {
            if (_prevTextBuffer == String.Empty)
                return;

            textBox1.Text = _prevTextBuffer;
            _prevTextBuffer = textBox1.Text;
            textBox1.ScrollToCaret();
        }

        private void editCut_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength < 1)
                return;

            int sStart = textBox1.SelectionStart;
            _copyBuffer = textBox1.SelectedText;
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
            textBox1.SelectionStart = sStart;
            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
        }

        private void editCopy_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength < 1)
                return;

            _copyBuffer = textBox1.SelectedText;
            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
        }

        private void editPaste_Click(object sender, EventArgs e)
        {
            int sStart = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, _copyBuffer);
            textBox1.SelectionStart = sStart + _copyBuffer.Length;
            textBox1.ScrollToCaret();
        }

        private void editDuplicate_Click(object sender, EventArgs e)
        {
            int sStart = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, textBox1.SelectedText);
            textBox1.SelectionStart = sStart;
            textBox1.ScrollToCaret();
        }

        private void editSelectAll_Click(object sender, EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
    }
}
