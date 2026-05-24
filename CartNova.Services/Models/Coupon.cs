using System.ComponentModel.DataAnnotations;

namespace CartNova.Services.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId{ get; set; }
        [Required]
        public string couponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }  
    }
}
