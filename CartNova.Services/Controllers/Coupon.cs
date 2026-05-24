using AutoMapper;
using CartNova.Services.DTO;
using CartNova.Services.Models;
using CartNova.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CartNova.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Coupon : ControllerBase
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        public Coupon(ICouponRepository couponRepository,IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        #region GET Requests
        [HttpGet]
        public IEnumerable<CouponDTO> Get()
        {
            var res = couponRepository.GetAll();
            if (res != null)
            {
                var result = mapper.Map<IEnumerable<CouponDTO>>(res);
                return result;
            }
            else
                return null;
        }

        [HttpGet]
        [Route("{id}")]
        public CouponDTO Get(int id)
        {
            var res = couponRepository.GetById(id);
            return mapper.Map<CouponDTO>(res);
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public CouponDTO GetByCode(string code)
        {
            var res = couponRepository.GetByCode(code);
            return mapper.Map<CouponDTO>(res);
        }
        #endregion

        #region POST Requests
        [HttpPost]
        public CouponDTO AddCoupon([FromBody] CouponDTO coupon) {
            try
            {
                Models.Coupon coupon1 = mapper.Map<Models.Coupon>(coupon);
                if (coupon1 == null)
                {
                    return null;
                }
                Models.Coupon returnedVal = couponRepository.AddCoupon(coupon1);
                return mapper.Map<CouponDTO>(returnedVal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region PUT Requests
        [HttpPut]
        public CouponDTO Edit([FromBody] CouponDTO couponDTO)
        {
            try
            {
                if (couponDTO == null) return null;
                Models.Coupon existingCoupon = couponRepository.GetById(couponDTO.CouponId);
                if (existingCoupon == null) return null;
                //var coupon = existingCoupon;
                existingCoupon = mapper.Map<Models.Coupon>(couponDTO);
                Models.Coupon updatedCoupon = couponRepository.updateCoupon(existingCoupon);
                return mapper.Map<CouponDTO>(updatedCoupon);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region Delete Requests
        [HttpDelete]
        public IActionResult DeleteCoupon([FromBody] DeleteCouponRequest request)
        {
            try
            {
                if(request.Id == null) return BadRequest("Invalid coupon ID");
                var existingCoupon = couponRepository.GetById(request.Id);
                if (existingCoupon == null) return NotFound("Coupon not found");
                return couponRepository.DeleteCoupon(request.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion
    }
}
