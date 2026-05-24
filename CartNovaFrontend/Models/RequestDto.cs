using static CartNovaFrontend.Utility.SD;

namespace CartNovaFrontend.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public Object? Data { get; set; }
        public string AccessToken { get; set; } 
    }
}
