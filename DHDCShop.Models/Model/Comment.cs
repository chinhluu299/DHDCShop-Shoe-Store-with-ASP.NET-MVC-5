using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public string CustomerUsername { get; set; }
        [DataType(DataType.Time)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("CustomerUsername")]
        public virtual Customer Customer { get; set; }
    }
}
