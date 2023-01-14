    using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public OrderState status { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }

        [ForeignKey("MobileOrder")]
        public int MobileId { get; set; }
        public virtual Mobile MobileOrder { get; set; }
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual IdentityUser Users { get; set; }
    }
    public enum OrderState
    {
        InCart,
        OrderPlaced,
        Verifying,
        Inprocess,
        Delivered
    }
}
