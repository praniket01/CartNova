
using CartNova.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CartNova.Services.Data
{
    public class CouponDBContext : DbContext
    {

        public CouponDBContext(DbContextOptions<CouponDBContext> options) : base(options)
        {
            
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                couponCode = "SAVE10",
                DiscountAmount = 10.0,
                MinAmount = 50
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                couponCode = "SAVE10",
                DiscountAmount = 8.0,
                MinAmount = 40
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                couponCode = "SAVE9",
                DiscountAmount = 9.0,
                MinAmount = 50
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 4,
                couponCode = "SAVE5",
                DiscountAmount = 6.0,
                MinAmount = 30
            });

        }
    }
}
