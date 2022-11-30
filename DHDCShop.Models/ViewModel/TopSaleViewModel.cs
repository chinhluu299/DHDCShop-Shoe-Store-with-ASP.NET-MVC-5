using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.ViewModel
{
    public class TopSaleViewModel
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
