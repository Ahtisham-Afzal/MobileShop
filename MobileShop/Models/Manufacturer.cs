using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MobileShop.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        public List<Mobile> mobiles { get; set; }
    }
}
