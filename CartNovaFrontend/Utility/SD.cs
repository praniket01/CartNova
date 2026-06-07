namespace CartNovaFrontend.Utility
{
    public class SD
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public static string Token = "Token";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
