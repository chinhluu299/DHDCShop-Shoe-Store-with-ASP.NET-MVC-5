using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.ViewModel
{
    [Serializable]
    public class CartItemViewModel
    {
        public Product Product { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
    }
}
