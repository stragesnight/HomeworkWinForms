using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_03
{
    public partial class Form1 : Form
    {
        // цена к оплате за бензин
        private float _gasCost = 0;
        // цена к оплате в мини-кафе
        private float _foodCost = 0;
        // общая стоимость
        private float _totalCost
        {
            get { return _gasCost + _foodCost; }
        }

        // типы бензина
        private List<GasType> _gasTypes;
        // выбранный тип бензина
        private GasType _selectedGasType = null;

        // реализация Singleton
        public static Form1 Instance;

        public Form1()
        {
            InitializeComponent();
        }

        // Обновить цену к оплате за бензин
        private void UpdateGasCost()
        {
            if (_selectedGasType == null)
                return;

            if (float.TryParse(textBox1.Text, out float gasV))
            {
                // прочитать объем из поля
                _gasCost = _selectedGasType.Price * gasV;
                label4.ForeColor = Color.Black;
                // вывести информацию о стоимости
                label4.Text = String.Format(
                    "{0:F2} грн/л\nx\n{1:F2} л\n"
                    + "-------------------\n{2:F2} грн",
                    _selectedGasType.Price, gasV, _gasCost);
            }
            else
            {
                // вывести ошибку
                label4.ForeColor = Color.Red;
                label4.Text = "ошибка";
            }
        }

        // Обновить цены всех категорий
        private void UpdateCosts()
        {
            UpdateGasCost();

            label5.Text = String.Format("{0:F2}", _gasCost);
            label8.Text = String.Format("{0:F2}", _foodCost);
            label10.Text = String.Format("{0:F2}", _totalCost);
        }

        // Инициализировать типы бензина
        private void InitializeGasTypes()
        {
            _gasTypes = new List<GasType> {
                new GasType{ Name = "Газ", Price = 19.07f },
                new GasType{ Name = "Дизель", Price = 30.10f },
                new GasType{ Name = "A-92", Price = 30.09f },
                new GasType{ Name = "A-95", Price = 30.86f },
                new GasType{ Name = "A-95*", Price = 32.80f }
            };

            comboBox1.Items.Clear();
            // выбрать имена типов и добавить их как элементы ComboBox
            comboBox1.Items.AddRange(_gasTypes.Select(x => x.Name).ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // обновить Singleton
            Form1.Instance = this;
            // Подписаться на событие изменения цены в кафе
            MiniCafe.Instance.CostChanged += MiniCafe_CostChanged;

            // инициализировать компоненты формы
            this.Text = "ДЗ №3, Шелест";
            groupBox1.Text = "Автозаправка";
            groupBox2.Text = "Мини-кафе";
            groupBox3.Text = "Всего к оплате";
            groupBox4.Text = "К оплате";
            groupBox5.Text = "К оплате";       

            label1.Text = "Бензин:";
            label2.Text = String.Empty;
            label3.Text = "Объем (л):";
            label4.Text = String.Empty;

            label2.Font = new Font(label2.Font.FontFamily, label2.Font.Size / 1.1f);
            label4.Font = new Font(label4.Font.FontFamily, label4.Font.Size / 1.1f);
            label5.Font = new Font(label5.Font.FontFamily, label5.Font.Size * 2f);
            label8.Font = new Font(label8.Font.FontFamily, label8.Font.Size * 2f);
            label10.Font = new Font(label10.Font.FontFamily, label10.Font.Size * 2f);
            label11.Font = new Font(label11.Font.FontFamily, label11.Font.Size / 1.1f);
            label12.Font = new Font(label12.Font.FontFamily, label12.Font.Size / 1.1f);
            label13.Font = new Font(label13.Font.FontFamily, label13.Font.Size / 1.1f);

            label6.Text = "грн";
            label7.Text = "грн";
            label9.Text = "грн";
            label11.Text = "Имя товара";
            label12.Text = "Цена";
            label13.Text = "Кол-во";
            UpdateCosts();
            button1.Text = "Получить\nчек";

            InitializeGasTypes();

            // добавить продукты в список мини-кафе
            flowLayoutPanel1.AutoScroll = true;
            MiniCafe.Instance.AddItem("Хот-дог", 41f);
            MiniCafe.Instance.AddItem("Гамбургер", 54.3f);
            MiniCafe.Instance.AddItem("Картошка-фри", 25.20f);
            MiniCafe.Instance.AddItem("Кока-кола", 35.40f);
            MiniCafe.Instance.AddItem("Вода", 13f);
            MiniCafe.Instance.AddItem("Жевачка", 23.12f);
            MiniCafe.Instance.AddItem("Сникерс", 43.9f);
            MiniCafe.Instance.AddItem("Капуччино", 48.3f);
            MiniCafe.Instance.AddItem("Эспрессо", 31.1f);
            MiniCafe.Instance.AddItem("Чай зелёный", 28.5f);
            MiniCafe.Instance.AddItem("Чай чёрный", 28.5f);
        }

        // Срабатывает при изменении цены к оплате в мини-кафе
        private void MiniCafe_CostChanged(object sender, EventArgs e)
        {
            _foodCost = MiniCafe.Instance.Cost;
            UpdateCosts();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= _gasTypes.Count())
                return;

            // обновить данные о цене бензина
            _selectedGasType = _gasTypes[comboBox1.SelectedIndex];
            label2.Text = String.Format("цена: {0:F2} грн/л", _selectedGasType.Price);

            UpdateCosts();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateCosts();
        }

        // Вывести чек к оплате на заправке
        private void button1_Click(object sender, EventArgs e)
        {
            // если нету данных о покупке бензина, вывести ошибку
            if (_selectedGasType == null || _gasCost == 0)
            {
                MessageBox.Show("Некорректные данные", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            // вывести информацию о покупке в MessageBox
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Заправка \"Шелестун\"");
            sb.AppendLine($"Чек №000001 от {DateTime.Now:dd.MM.yyyy}\n");
            sb.AppendLine("============================");
            sb.AppendLine("ЗАПРАВКА");
            sb.AppendLine(String.Format("Бензин:\n{0,64}", _selectedGasType.Name));
            sb.AppendLine(String.Format("Цена за 1 литр:\n{0,64:F2} грн", _selectedGasType.Price));
            sb.AppendLine(String.Format("Объем:\n{0,64:F2} л\n", _gasCost / _selectedGasType.Price));
            sb.AppendLine(String.Format("К оплате:\n{0,64:F2} грн", _gasCost));
            MiniCafe.Instance.GetReceiptString(sb);
            sb.AppendLine("============================");
            sb.AppendLine(String.Format("\nВСЕГО К ОПЛАТЕ:\n{0,64:F2} грн", _totalCost));

            MessageBox.Show(sb.ToString(), "Чек", MessageBoxButtons.OK);
        }
    }
}
