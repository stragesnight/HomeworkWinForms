using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_06
{
    public partial class ProductList : Form
    {
        public ProductList()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Список товаров";
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            this.Text = "Список товаров";
            button1.Text = "Добавить";
            button2.Text = "Изменить";
            button3.Text = "Удалить";
            button4.Text = "Очистить";

            this.Font = new Font(this.Font.FontFamily, this.Font.Size * 1.2f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductListItem item = new ProductListItem();
            ProductListDialog dialog = new ProductListDialog(item, true);

            this.Hide();
            if (dialog.ShowDialog() == DialogResult.OK)
                listBox1.Items.Add(item);
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;

            if (i == -1)
            {
                MessageBox.Show("Нету выбранного товара");
                return;
            }

            ProductListItem item = listBox1.SelectedItem as ProductListItem;
            ProductListDialog dialog = new ProductListDialog(item, false);

            this.Hide();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.RemoveAt(i);
                listBox1.Items.Insert(i, item);
            }
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Нету выбранного товара");
                return;
            }

            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
