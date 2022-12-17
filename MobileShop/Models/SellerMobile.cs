using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShop.Models
{
    public class SellerMobile
    {
        [Key]
        public int Id { get; set; }

        //--------------------

        [ForeignKey("Mobiles")]
        public int MobileId { get; set; }
        public Mobile Mobiles { get; set; }

        //--------------------

        [ForeignKey("Sellers")]
        public int SellerId { get; set; }
        public Seller Sellers { get; set; }
    }
}
