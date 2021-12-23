using System;
using System.Windows.Forms;

namespace WINFORMS_DZ_10
{
    public partial class MainForm : Form
    {
        private void InitializeCustomizeMenu()
        {
            var customizeMenu = menuStrip1.Items.Add("Customize...");
            customizeMenu.Click += new EventHandler(customizeMenu_Click);
        }

        private void InitializeAboutMenu()
        {
            var aboutMenu = menuStrip1.Items.Add("About...");
            aboutMenu.Click += new EventHandler(aboutMenu_Click);
        }

        private void customizeMenu_Click(object sender, EventArgs e)
        {
            (new CustomizationForm()).ShowDialog();
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Text Editor by Shelest Alexander\n"
                + "Made as an homework assignment for academy,\n"
                + "December, 2021", "About");
        }
    }
}
