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
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Ratings = new HashSet<Rating>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal TotalMoney { get; set; }
        [Required]
        public int StatusId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        
        public string FeedBack { get; set; }
        [Required]
        public string NameOfReceiver { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string NumberPhoneRev { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailRev { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsPaid { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
