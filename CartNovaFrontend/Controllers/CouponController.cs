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

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateUpdateCouponAsync(couponDto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(couponDto);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {

            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDto? couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(couponDto);
            }
            return NotFound();

        }

        [HttpPost]
        //[Route("{id:int}")]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {

            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(couponDto);
        }
    }

}
