using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Model
{
    public class Customer
    {
        public Customer()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
            WishLists = new HashSet<WishList>();
            Ratings = new HashSet<Rating>();
        }
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Nation { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfRegister { get; set; }

        public string AvatarPath { get; set; }
        public decimal TotalSpent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
      
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<WishList> WishLists { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
