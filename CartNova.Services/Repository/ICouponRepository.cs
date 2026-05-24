using CartNova.Services.DTO;
using CartNova.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartNova.Services.Repository
{
    public interface ICouponRepository
    {
        Coupon GetById(int id);
        IEnumerable<Coupon> GetAll();
        Coupon GetByCode(string code);
        Coupon AddCoupon(Coupon coupon);
        Coupon updateCoupon(Coupon coupon);
        IActionResult DeleteCoupon(int id);
    }
}
