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
    public partial class ProductListDialog : Form
    {
        private ProductListItem _item;
        private bool _addItem;

        public ProductListDialog(ProductListItem item, bool addItem)
        {
            InitializeComponent();

            _item = item;
            _addItem = addItem;
        }

        private void InventoryListDialog_Load(object sender, EventArgs e)
        {
            this.Text = "Добавление товара";
            label1.Text = "Название";
            label2.Text = "Производитель";
            label3.Text = "Цена";
            button1.Text = "OK";
            button2.Text = "Отмена";

            if (!_addItem)
            {
                this.Text = "Изменение товара";
                textBox1.Text = _item.Name;
                textBox2.Text = _item.Manufacturer;
                textBox3.Text = String.Format("{0:F2}", _item.Price);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool valid = true;
            _item.Name = textBox1.Text;
            _item.Manufacturer = textBox2.Text;
            bool validPrice = float.TryParse(textBox3.Text, out float price);

            valid &= _item.Name != String.Empty;
            valid &= _item.Manufacturer != String.Empty;
            valid &= validPrice;

            try
            {
                if (!valid)
                    throw new Exception("Введены некорректные данные");
                _item.Price = price;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при отправке данных:\n"
                    + ex.Message);

                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
