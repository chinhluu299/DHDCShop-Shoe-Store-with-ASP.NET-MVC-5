using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class ProductSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
