using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WINFORMS_DZ_03
{
    // Товар в мини-кафе
    public class MiniCafeItem
    {
        // Событие изменения состояния товара в списке
        public event EventHandler CostChanged;

        // имя товара
        public string Name { get; set; }
        // цена товара за штуку
        public float Price { get; set; }
        // количество товара в заказе
        public int Quantity { get; private set; }
        private bool _checked = false;
        public bool Checked 
        {
            get => _checked;
            set
            {
                _checked = value;
                CheckedChanged();
            }
        }
        // общая цена за товар
        public float Cost
        {
            get => Checked ? Price * Quantity : 0f;
        }

        // элементы формы

        private TextBox _priceTextBox;
        private TextBox _quantityTextBox;

        public MiniCafeItem(Control formParent, string Name, float Price)
        {
            this.Name = Name;
            this.Price = Price;
            this.Quantity = 0;

            // инициализация новых компонентов формы
            _priceTextBox = new TextBox();
            _quantityTextBox = new TextBox();

            formParent.Controls.Add(_priceTextBox);
            formParent.Controls.Add(_quantityTextBox);

            _priceTextBox.Text = String.Format("{0:F2}", Price);
            _priceTextBox.Enabled = false;
            _priceTextBox.Location = new Point(3, 3);
            _priceTextBox.Size = new Size(51, 22);
            _priceTextBox.TabIndex = 0;
            _priceTextBox.TextAlign = HorizontalAlignment.Right;

            _quantityTextBox.Text = "0";
            _quantityTextBox.Enabled = false;
            _quantityTextBox.Location = new Point(60, 3);
            _quantityTextBox.Size = new Size(35, 22);
            _quantityTextBox.TabIndex = 1;
            _quantityTextBox.TextAlign = HorizontalAlignment.Right;
            _quantityTextBox.TextChanged += quantityTextBox_ValueChanged;
        }

        private void CheckedChanged()
        {
            _quantityTextBox.Enabled = Checked;
            // вызвать событие изменения состояния товара
            CostChanged?.Invoke(this, new EventArgs());
        }

        private void quantityTextBox_ValueChanged(object sender, EventArgs e)
        {
            _quantityTextBox.ForeColor = Color.Black;
            // прочитать количество товара в заказе
            if (int.TryParse(_quantityTextBox.Text, out int result))
                Quantity = result;         
            else
                // изменить цвет текста на красный, если возникла ошибка
                _quantityTextBox.ForeColor = Color.Red;

            // вызвать событие изменения состояния товара
            CostChanged?.Invoke(this, new EventArgs());
        }
    }
}

