using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShop.Models
{
    public class MobileSeller
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Mobiles")]
        public int MobileId { get; set; }
        public Mobile Mobiles { get; set; }

        //--------------------

        [ForeignKey("Sellers")]
        public int ManufacturerId { get; set; }
        public Seller Sellers { get; set; }
    }
}
