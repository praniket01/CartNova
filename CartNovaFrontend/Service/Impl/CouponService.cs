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

        public Task<ResponseDto> CreateUpdateCouponAsync(CouponDto couponDto, string token)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon",
                AccessToken = token
            });
        }

        public Task<ResponseDto> DeleteCouponAsync(DeleteCouponDto couponDto,string token)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/",
                Data = couponDto.Id,
                AccessToken = token
            });
        }

        public async Task<ResponseDto> GetAllCouponsAsync(string token)
        {
           return await _baseService.SendAsync(new RequestDto
           {
               ApiType = SD.ApiType.GET,
               Url = SD.CouponAPIBase + "/api/coupon",
               AccessToken = token
           });
        }

        public Task<ResponseDto> GetCouponByIdAsync(int id, string token)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id,
                AccessToken = token
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
