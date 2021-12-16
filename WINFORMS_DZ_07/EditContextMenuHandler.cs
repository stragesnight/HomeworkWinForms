using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_07
{
    public partial class MainForm : Form
    {
        private void InitializeEditContextMenuFor(Control control)
        {
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add("Undo", new EventHandler(editUndo_Click));
            contextMenu.MenuItems.Add("Redo", new EventHandler(editRedo_Click));
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Cut", new EventHandler(editCut_Click));
            contextMenu.MenuItems.Add("Copy", new EventHandler(editCopy_Click));
            contextMenu.MenuItems.Add("Paste", new EventHandler(editPaste_Click));
            contextMenu.MenuItems.Add("Duplicate", new EventHandler(editDuplicate_Click));
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Select All", new EventHandler(editSelectAll_Click));

            control.ContextMenu = contextMenu;
        }
    }
}
