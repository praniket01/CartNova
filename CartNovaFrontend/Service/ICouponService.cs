using CartNovaFrontend.Models;

namespace CartNovaFrontend.Service
{
    public interface ICouponService
    {
        Task<ResponseDto> GetAllCouponsAsync();
        Task<ResponseDto> GetCouponByIdAsync(int id);
        Task<ResponseDto> CreateUpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto> DeleteCouponAsync(int CouponId);
        Task<ResponseDto> UpdateCouponAsync(CouponDto requestDto, string token);
    }
}
