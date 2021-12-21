using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WINFORMS_DZ_09
{
    public partial class Form1 : Form
    {
        private MenuStrip menuStrip;
        private string _currentFileName = "";
        private const string filenameFilters = ""
            + "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|"
            + "JPG Images (*.jpg)|*.jpg|"
            + "JPEG Images (*.jpeg)|*.jpeg|"
            + "PNG Images (*.png)|*.png|"
            + "BMP Images (*.bmp)|*.bmp|"
            + "All Files (*.*)|*.*";

        private void InitializeMenuStrip()
        {
            this.SuspendLayout();

            menuStrip = new MenuStrip();
            this.Controls.Add(menuStrip);

            var fileMenu = (ToolStripMenuItem)menuStrip.Items.Add("File");

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

            this.MainMenuStrip = menuStrip;
            this.ResumeLayout(true);
        }

        private ImageFormat GetImageFormat(string filename)
        {
            int doti = filename.LastIndexOf('.');
            string fileformat = filename.Substring(
                doti + 1, filename.Length - doti - 1
            );

            if (fileformat == "jpg" || fileformat == "jpeg")
                return ImageFormat.Jpeg;
            else if (fileformat == "png")
                return ImageFormat.Png;
            else if (fileformat == "bmp")
                return ImageFormat.Bmp;
            else
                throw new Exception("Unrecognized image format");
        }

        private void fileNewFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = filenameFilters;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            _currentFileName = ofd.FileName;
        }

        private void fileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filenameFilters;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            _paintingEngine.SetImage(ofd.FileName);
        }

        private void fileSave_Click(object sender, EventArgs e)
        {
            if (_currentFileName == String.Empty)
            {
                fileSaveAs_Click(this, new EventArgs());
                return;
            }

            _paintingEngine.SaveImage(_currentFileName, GetImageFormat(_currentFileName));
        }

        private void fileSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filenameFilters;

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            _paintingEngine.SaveImage(sfd.FileName, GetImageFormat(sfd.FileName));
        }

        private void fileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
