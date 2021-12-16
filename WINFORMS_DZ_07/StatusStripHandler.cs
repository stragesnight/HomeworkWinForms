using System;
using System.Text;
using System.Windows.Forms;

namespace WINFORMS_DZ_07
{
    public partial class MainForm : Form
    {
        private ToolStripItem statusStripFilenameItem;
        private ToolStripItem statusStripEncodingItem;
        private ToolStripItem statusStripFileStatsItem;

        private void InitializeStatusStrip()
        {
            statusStripFilenameItem = statusStrip1.Items.Add("filename");
            statusStripEncodingItem = statusStrip1.Items.Add("encoding");
            statusStripFileStatsItem = statusStrip1.Items.Add("file stats");

            statusStripFilenameItem.Alignment = ToolStripItemAlignment.Left;
            statusStripEncodingItem.Alignment = ToolStripItemAlignment.Left;
            statusStripFileStatsItem.Alignment = ToolStripItemAlignment.Right;

            UpdateStatusStrip();
        }

        private void UpdateStatusStrip()
        {
            if (_currentFileName == String.Empty)
            {
                statusStripFilenameItem.Text = String.Empty;
                statusStripEncodingItem.Text = String.Empty;
                statusStripFileStatsItem.Text = String.Empty;
                return;
            }

            int i = _currentFileName.LastIndexOf('\\') + 1;
            statusStripFilenameItem.Text = _currentFileName.Substring(i, _currentFileName.Length - i);
            if (_hasUnsavedChanges)
                statusStripFilenameItem.Text += "*";

            statusStripEncodingItem.Text = Encoding.Default.EncodingName;
            statusStripFileStatsItem.Text = String.Format("{0,32}:{1}",
                textBox1.SelectionStart, textBox1.Text.Length);
        }
    }
}
