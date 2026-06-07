using CartNovaFrontend.Models;

namespace CartNovaFrontend.Service
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(Models.RequestDto requestDto, bool withBearer = true);
    }
}
