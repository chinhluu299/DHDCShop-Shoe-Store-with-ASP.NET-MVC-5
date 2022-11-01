using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Statistic
    {
        public int Id { get; set; }
        [Required]
        [DefaultValue(0)]
        [DataType(DataType.Currency)]
        public decimal TotalRevenue { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [DefaultValue(0)]
        public int NumberOfOrder { get; set; }
        [Required]
        [DefaultValue(0)]
        public int NumberOfNewRegister { get; set; }
        [Required]
        [DefaultValue(0)]
        public decimal NumberOfSales{ get; set; }
    }
}
