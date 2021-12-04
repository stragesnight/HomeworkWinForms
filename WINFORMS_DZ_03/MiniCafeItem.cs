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
        // общая цена за товар
        public float Cost
        {
            get => _checkBox.Checked ? Price * Quantity : 0f;
        }

        // элементы формы

        private Control _formParent;
        private CheckBox _checkBox;
        private Label _label;
        private TextBox _priceTextBox;
        private TextBox _quantityTextBox;

        public MiniCafeItem(Control formParent, string Name, float Price)
        {
            this.Name = Name;
            this.Price = Price;
            this.Quantity = 0;

            // инициализация новых компонентов формы
            _checkBox = new CheckBox();
            _label = new Label();
            _priceTextBox = new TextBox();
            _quantityTextBox = new TextBox();
            formParent.SuspendLayout();

            _formParent = formParent;
            _formParent.Controls.Add(_checkBox);
            _formParent.Controls.Add(_label);
            _formParent.Controls.Add(_priceTextBox);
            _formParent.Controls.Add(_quantityTextBox);

            _checkBox.Checked = false;
            _checkBox.AutoSize = true;
            _checkBox.Location = new Point(3, 8);
            _checkBox.Size = new Size(15, 14);
            _checkBox.TabIndex = 0;
            _checkBox.UseVisualStyleBackColor = true;
            _checkBox.CheckedChanged += checkBox_CheckedChanged;

            _label.AutoSize = true;
            _label.Location = new Point(24, 6);
            _label.Size = new Size(52, 16);
            _label.TabIndex = 1;
            _label.Text = Name;
            _label.Font = new Font(_label.Font.FontFamily, _label.Font.Size / 1.1f);

            _priceTextBox.Text = String.Format("{0:F2}", Price);
            _priceTextBox.Enabled = false;
            _priceTextBox.Location = new Point(133, 3);
            _priceTextBox.Size = new Size(51, 22);
            _priceTextBox.TabIndex = 2;
            _priceTextBox.TextAlign = HorizontalAlignment.Right;

            _quantityTextBox.Text = "0";
            _quantityTextBox.Enabled = false;
            _quantityTextBox.Location = new Point(190, 3);
            _quantityTextBox.Size = new Size(35, 22);
            _quantityTextBox.TabIndex = 3;
            _quantityTextBox.TextAlign = HorizontalAlignment.Right;
            _quantityTextBox.TextChanged += quantityTextBox_ValueChanged;

            _formParent.ResumeLayout(true);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            _quantityTextBox.Enabled = _checkBox.Checked;
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

