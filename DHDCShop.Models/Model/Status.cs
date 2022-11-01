using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Status
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
