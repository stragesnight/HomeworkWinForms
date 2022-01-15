using System;
using System.IO;
using System.Windows.Forms;

namespace WINFORMS_DZ_15
{
    public partial class MainForm : Form
    {
        private void InitializeFileMenuStrip()
        {
            var fileMenu = (ToolStripMenuItem)menuStrip1.Items.Add("File");

            var fileNewFile = (ToolStripMenuItem)fileMenu.DropDownItems.Add("New file");
            fileNewFile.ShortcutKeys = Keys.Control | Keys.N;
            fileNewFile.ShowShortcutKeys = true;
            fileNewFile.Click += new EventHandler(fileNewFile_Click);

            var fileOpen = (ToolStripMenuItem)fileMenu.DropDownItems.Add("Open");
            fileOpen.ShortcutKeys = Keys.Control | Keys.O;
            fileOpen.ShowShortcutKeys = true;
            fileOpen.Click += new EventHandler(fileOpen_Click);

            fileMenu.DropDownItems.Add(new ToolStripSeparator());

            var fileSave = (ToolStripMenuItem)fileMenu.DropDownItems.Add("Save");
            fileSave.ShortcutKeys = Keys.Control | Keys.S;
            fileSave.ShowShortcutKeys = true;
            fileSave.Click += new EventHandler(fileSave_Click);

            var fileSaveAs = (ToolStripMenuItem)fileMenu.DropDownItems.Add("Save as...");
            fileSaveAs.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
            fileSave.ShowShortcutKeys = true;
            fileSaveAs.Click += new EventHandler(fileSaveAs_Click);

            fileMenu.DropDownItems.Add(new ToolStripSeparator());

            var fileClose = (ToolStripMenuItem)fileMenu.DropDownItems.Add("Close");
            fileClose.ShortcutKeys = Keys.Control | Keys.Shift | Keys.X;
            fileClose.ShowShortcutKeys = true;
            fileClose.Click += new EventHandler(fileClose_Click);
        }

        private void fileNewFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            _currentFileName = ofd.FileName;
            textBox1.Text = String.Empty;

            _hasUnsavedChanges = false;
        }

        private void fileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            using (StreamReader sr = File.OpenText(ofd.FileName))
            {
                _currentFileName = ofd.FileName;
                textBox1.Text = sr.ReadToEnd();
            }

            _hasUnsavedChanges = false;
        }

        private void fileSave_Click(object sender, EventArgs e)
        {
            if (_currentFileName == String.Empty)
            {
                fileSaveAs_Click(this, new EventArgs());
                return;
            }

            using (StreamWriter sw = new StreamWriter(_currentFileName))
                sw.Write(textBox1.Text);

            _hasUnsavedChanges = false;
        }

        private void fileSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            _currentFileName = sfd.FileName;
            using (StreamWriter sw = new StreamWriter(_currentFileName))
                sw.Write(textBox1.Text);

            _hasUnsavedChanges = false;
        }

        private void fileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HandleClosedForm(object sender, EventArgs e)
        {
            if (!_hasUnsavedChanges)
                return;

            DialogResult result = MessageBox.Show("Do you want to save current file?",
                                        "Close program",
                                        MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                fileSave_Click(this, new EventArgs());
        }
    }
}
