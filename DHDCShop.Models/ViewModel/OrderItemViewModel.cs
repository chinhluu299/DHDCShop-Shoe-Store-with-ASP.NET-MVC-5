using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.ViewModel
{
    public class OrderItemViewModel
    {
        public List<CartItemViewModel> ListCartItem { get; set; }
        public Order Order { get; set; }
    }
}
