using CartNovaFrontend.Models;
using CartNovaFrontend.Service;
using CartNovaFrontend.Utility;

namespace CartNovaFrontend.Service.Impl
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService _baseService)
        {
            this._baseService = _baseService;
        }

        public Task<ResponseDto> CreateUpdateCouponAsync(CouponDto couponDto)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon",
            });
        }

        public Task<ResponseDto> DeleteCouponAsync(int couponDto)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/"+couponDto,
            });
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
           return await _baseService.SendAsync(new RequestDto
           {
               ApiType = SD.ApiType.GET,
               Url = SD.CouponAPIBase + "/api/coupon",
           });
        }

        public Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id,
            });
        }

        public Task<ResponseDto> UpdateCouponAsync(CouponDto requestDto, string token)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = requestDto,
                Url = SD.CouponAPIBase + "/api/coupon",
                AccessToken = token
            });
        }

    }
}
