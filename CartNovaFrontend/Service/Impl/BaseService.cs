using System.Text;
using CartNovaFrontend.Models;
using Newtonsoft.Json;
using CartNovaFrontend.Utility;
using static CartNovaFrontend.Utility.SD;
using System.Net;

namespace CartNovaFrontend.Service.Impl
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("CartNovaAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Url != null)
                {
                    message.RequestUri = new Uri(requestDto.Url);
                }

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    if (token!= null)
                    {
                        message.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }

                HttpResponseMessage apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await httpClient.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return dto;

            }
        }
    }
}
