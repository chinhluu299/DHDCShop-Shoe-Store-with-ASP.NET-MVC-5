using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DHDCShop.Models.Model
{
    public class Product
    {
        public Product()
        {
            ProductSizes = new HashSet<ProductSize>();

            Comments = new HashSet<Comment>();

            OrderDetails = new HashSet<OrderDetail>();

            ProductImages = new HashSet<ProductImage>();

            SaleOffs = new HashSet<SaleOff>();

            WishLists = new HashSet<WishList>();

            Ratings = new HashSet<Rating>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Brand { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        [DefaultValue(0)]
        public int Quantity { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DefaultValue(0)]
        public decimal Rating { get; set; }
        [DefaultValue(0)]
        public int NumOfVote { get; set; }

       
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
     
        public virtual ICollection<SaleOff> SaleOffs { get; set; }

        [JsonIgnore]
        public virtual ICollection<WishList> WishLists { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
