using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatePost { get; set; }
        [Required]
        public string Content { get; set; }
        
        public string ImagePath { get; set; }
    }
}
