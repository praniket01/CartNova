using AutoMapper;
using CartNova.Services.DTO;
using CartNova.Services.Models;

namespace CartNova.Services.MappingConfig
{
    public class AutoMapperConfig
    {
        //Copilot's, Implemented AutoMapper configuration here
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfigurationExpression mpe = new MapperConfigurationExpression();
            mpe.CreateMap<Coupon, CouponDTO>();
            mpe.CreateMap<CouponDTO, Coupon>();
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            MapperConfiguration mappingConfig = new MapperConfiguration(mpe,loggerFactory);

            return mappingConfig;
        }

    }
}
