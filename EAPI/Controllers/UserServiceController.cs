using EAPI.Behaviour.Interfaces;
using EAPI.Data;
using EAPI.Exceptions;
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
    public class UserServiceController : ControllerBase
    {
        private readonly IUserControlBehaviour controlBehaviour;
        public UserServiceController(IUserControlBehaviour behaviour)
        {
            controlBehaviour = behaviour;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserItem user)
        {
            try
            {
                return Ok(controlBehaviour.Register(user));
            }
            catch (DuplicateRecordError ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,new {Error= ex.Message});
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = e.Message });
            }
        }
        [HttpGet("get-users")]
        public IActionResult GetUsers() 
        {
            try
            {
                return Ok(controlBehaviour.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new { Error=ex.Message});
            }
            
        }
    }
}
