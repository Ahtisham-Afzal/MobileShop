using System.Collections.Generic;

namespace MobileShop.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Mobile> Mobiles { get; set; }
    }
}
