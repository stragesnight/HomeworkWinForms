using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WINFORMS_DZ_06
{
    public class ProductListItem
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }

        private float _price;
        public float Price 
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new Exception("Цена товара не может быть меньше ноля");
                _price = value;
            }
        }

        public ProductListItem()
        {
            Name = String.Empty;
            Manufacturer = String.Empty;
            Price = 0;
        }

        public ProductListItem(string name, string manufacturer, float price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }

        public override string ToString()
        {
            return Name + ", Производитель: " + Manufacturer 
                    + ", Цена: " + String.Format("{0:F2}", Price);
        }
    }
}
