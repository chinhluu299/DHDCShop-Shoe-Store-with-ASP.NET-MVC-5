using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Content { get; set; }

        public string Timestamp { get; set; }

        public string FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        public virtual Customer FromUser { get; set; }
       
        //type 1: user => admin
        //type 2: admin => user
        public int Type { get; set; }

        public int Stick { get; set; }
    }
}
