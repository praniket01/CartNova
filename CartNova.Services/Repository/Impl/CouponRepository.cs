using CartNova.Services.Data;
using CartNova.Services.DTO;
using CartNova.Services.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CartNova.Services.Repository.Impl
{
    public class CouponRepository : ICouponRepository
    {
        private readonly CouponDBContext dbContext;
        public CouponRepository(
            CouponDBContext dbContext
            )
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Coupon> GetAll()
        {
            var result = dbContext.Coupons.ToList();
            return result;


        }
        public Coupon GetById(int id)
        {
            var result = dbContext.Coupons.Find(id);
            if (id == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public Coupon GetByCode(string code)
        {
            try
            {
                if (code == null)
                {
                    return null;
                }
                var result = dbContext.Coupons.Where(c => c.couponCode == code).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Coupon AddCoupon(Coupon coupon)
        {
            try
            {
                dbContext.Coupons.Add(coupon);
                dbContext.SaveChanges();
                return coupon;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Coupon updateCoupon(Coupon coupon)
        {
            if(coupon == null)
            {
                return null;
            }

            var existingCoupon = dbContext.Coupons.Find(coupon.CouponId);
            existingCoupon = coupon;
            dbContext.SaveChanges(); 
            return existingCoupon;
        }

        public IActionResult DeleteCoupon(int id)
        {
            var existingCoupon = dbContext.Coupons.Find(id);
            if (existingCoupon == null)
            {
                return new NotFoundResult();
            }
            dbContext.Coupons.Remove(existingCoupon);
            dbContext.SaveChanges();
            return new OkObjectResult("Deleted SuccessFully");
        }
    }
    }
