using AutoMapper;
using CartNova.Services.DTO;
using CartNova.Services.Models;
using CartNova.Services.Repository;
using CartNovaFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartNova.Services.Controllers
{
    [Route("jls;faj/[controller]")]
    [ApiController]
    public class Coupon : ControllerBase
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;

        public Coupon(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        #region GET Requests

        [HttpGet]
        public ActionResult<ResponseDto> Get()
        {
            ResponseDto response = new();

            try
            {
                var res = couponRepository.GetAll();

                response.Result = mapper.Map<IEnumerable<CouponDTO>>(res);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            try
            {
                var res = couponRepository.GetById(id);

                if (res == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Coupon not found";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Result = mapper.Map<CouponDTO>(res);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("GetByCode/{code}")]
        public ActionResult<ResponseDto> GetByCode(string code)
        {
            ResponseDto response = new();

            try
            {
                var res = couponRepository.GetByCode(code);

                if (res == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Coupon not found";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Result = mapper.Map<CouponDTO>(res);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        #endregion

        #region POST Requests

        [HttpPost]
        public ActionResult<ResponseDto> AddCoupon([FromBody] CouponDTO coupon)
        {
            ResponseDto response = new();

            try
            {
                Models.Coupon couponEntity = mapper.Map<Models.Coupon>(coupon);

                if (couponEntity == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid coupon data";
                    return BadRequest(response);
                }

                response.IsSuccess = true;
                Models.Coupon returnedVal = couponRepository.AddCoupon(couponEntity);

                response.Result = mapper.Map<CouponDTO>(returnedVal);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        #endregion

        #region PUT Requests

        [HttpPut]
        public ActionResult<ResponseDto> Edit([FromBody] CouponDTO couponDTO)
        {
            ResponseDto response = new();

            try
            {
                if (couponDTO == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid request";
                    return BadRequest(response);
                }

                Models.Coupon existingCoupon =
                    couponRepository.GetById(couponDTO.CouponId);

                if (existingCoupon == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Coupon not found";
                    return NotFound(response);
                }

                existingCoupon = mapper.Map<Models.Coupon>(couponDTO);

                Models.Coupon updatedCoupon =
                    couponRepository.updateCoupon(existingCoupon);

                response.Result = mapper.Map<CouponDTO>(updatedCoupon);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        #endregion

        #region DELETE Requests

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<ResponseDto> DeleteCoupon(int id)
        {
            ResponseDto response = new();

            try
            {
                //if (request.Id == null)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Invalid coupon ID";
                //    return BadRequest(response);
                //}

                var existingCoupon = couponRepository.GetById(id);

                if (existingCoupon == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Coupon not found";
                    return NotFound(response);
                }

                couponRepository.DeleteCoupon(id);

                response.Result = true;
                response.IsSuccess = true;
                response.Message = "Coupon deleted successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        #endregion
    }
}