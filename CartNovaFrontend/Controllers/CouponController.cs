using CartNovaFrontend.Models;
using CartNovaFrontend.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CartNovaFrontend.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService _couponService)
        {
            this._couponService = _couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? couponDto = new List<CouponDto>();
            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            
            if(response != null && response.IsSuccess)
            {
                couponDto = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(couponDto);
        }
    }
}
