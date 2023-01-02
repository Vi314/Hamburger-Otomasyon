using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Hamburger.Concrete
{
    internal class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }

        public DateTime OrderDate { get; }
        public string Menu { get; set; }
        public int AmountOfMenus { get; set; }

        public List<string> OrderedExtras = new List<string>();
        public decimal OrderPrice { get; set; }


    }
}
