using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
        [Required]
        public string CustomerUsername { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int NumberOfStar { get; set; }
        [Required]
        public int OrderId { get; set; }


        [ForeignKey("CustomerUsername")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
