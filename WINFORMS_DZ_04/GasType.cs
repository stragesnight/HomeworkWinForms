using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WINFORMS_DZ_03
{
    class GasType : IComparable<GasType>
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public int CompareTo(GasType other) 
            => (other.Name == this.Name) ? 1 : 0;
    }
}
