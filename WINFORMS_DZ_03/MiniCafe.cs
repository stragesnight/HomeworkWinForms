using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace WINFORMS_DZ_03
{
    // Класс мини-кафе, ответственный за хранение списка продуктов
    // и сообщении форме об изменении их состояния
    class MiniCafe
    {
        // слбытие изменения общей цены в кафе
        public event EventHandler CostChanged;

        // цена к оплате в кафе
        public float Cost = 0;

        // Singleton
        private static MiniCafe _instance = null;
        public static MiniCafe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MiniCafe();
                return _instance;
            }
        }

        // список продуктов в кафе
        private List<MiniCafeItem> _items;

        private MiniCafe()
        {
            _items = new List<MiniCafeItem>();
        }

        // добавитб панель для нвого продукта на форму
        private Panel AddPanel()
        {
            Form1 form = Form1.Instance;
            FlowLayoutPanel layoutPanel = form.flowLayoutPanel1;

            layoutPanel.SuspendLayout();
            form.SuspendLayout();

            Panel panel = new Panel();
            layoutPanel.Controls.Add(panel);
            // выбрать положение в зависимости от количества элементов
            panel.Location = new Point(3, _items.Count * 28 + 3);
            panel.Size = new Size(226, 24);

            layoutPanel.ResumeLayout(true);
            form.ResumeLayout(true);
            return panel;
        }

        // Добавить товар в список
        public void AddItem(string name, float cost)
        {
            // инициализировать товар в мини-кафе
            MiniCafeItem item = new MiniCafeItem(AddPanel(), name, cost);
            // подписаться на событие изменения цены товара
            item.CostChanged += Item_CostChanged;
            // добавить товар в список
            _items.Add(item);
        }

        // получить чек от мини-кафе
        public void GetReceiptString(StringBuilder sb)
        {
            sb.AppendLine("============================");
            sb.AppendLine("МИНИ-КАФЕ");

            foreach (MiniCafeItem mci in _items)
            {
                if (mci.Quantity == 0)
                    continue;

                sb.AppendLine("\"" + mci.Name + "\"");
                sb.AppendLine(String.Format("Стоимость:\n{0,32} грн",
                    String.Format("{0:F2} x {1} = {2:F2}", mci.Price, mci.Quantity, mci.Cost)));
            }

            sb.AppendLine(String.Format("\nК оплате:\n{0,64:F2} грн", Cost));
        }

        // Выполняется при изменении состояния любого из объектов
        private void Item_CostChanged(object sender, EventArgs e)
        {
            // обновить общую цену к оплате за мини-кафе
            Cost = _items.Sum(x => x.Cost);
            // передлать событие форме
            CostChanged?.Invoke(this, new EventArgs());
        }
    }
}
