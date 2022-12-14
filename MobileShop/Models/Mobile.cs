using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShop.Models
{
    public class Mobile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int Price { get; set; }
        [ForeignKey("Manufacturers")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturers { get; set; }

        public List<Seller> Sellers { get; set; }
        //public List<Manufacturer> Manufacturers { get; set; }
    }
}
