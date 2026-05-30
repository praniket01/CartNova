using CartNovaFrontend.Models;

namespace CartNovaFrontend.Service
{
    public interface ICouponService
    {
        Task<ResponseDto> GetAllCouponsAsync();
        Task<ResponseDto> GetCouponByIdAsync(int id, string token);
        Task<ResponseDto> CreateUpdateCouponAsync(CouponDto couponDto, string token);
        Task<ResponseDto> DeleteCouponAsync(DeleteCouponDto couponDto, string token);
        Task<ResponseDto> UpdateCouponAsync(CouponDto requestDto, string token);
    }
}
