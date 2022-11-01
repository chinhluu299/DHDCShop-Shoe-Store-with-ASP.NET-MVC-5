using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class SaleOff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [DataType(DataType.Currency)]
        public decimal NewPrice { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DateStart { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DateEnd { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
