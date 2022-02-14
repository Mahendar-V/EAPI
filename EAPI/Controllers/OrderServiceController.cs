using EAPI.Behaviour.Interfaces;
using EAPI.Data;
using EAPI.DataAccess;
using EAPI.DataModels.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderServiceController : ControllerBase
    {
        private IOrderBehaviour _behaviour;
        public OrderServiceController(IOrderBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        [HttpPost("order")]
        public IActionResult Order([FromBody] OrderItem order)
        {
            try
            {
                return Ok(_behaviour.PlaceOrder(order));
            }
            catch(NotFoundError e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                           new { Error = e.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                            new { Error=ex.Message} );
            }
        }

        [HttpPost("bulk-order")]
        public IActionResult Order([FromBody] List<OrderItem> orders)
        {
            try
            {
                return Ok(_behaviour.PlaceOrder(orders));
            }
            catch (NotFoundError e)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                           new { Error = e.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                            new { Error = ex.Message });
            }
        }
        [HttpGet("get/{userId:int}")]
        public IActionResult GetOrders(int userId)
        {
            try
            {
                return Ok(_behaviour.GetOrders(userId));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                            new { Error = ex.Message });
            }
        }

        [HttpGet("get-ordersby-page")]
        public IActionResult GetPagedOrdersOrders(int psize, int pno)
        {
            try
            {
                return Ok(_behaviour.GetPagedOrders(psize, pno));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                            new { Error = ex.Message });
            }
        }
    }
}
