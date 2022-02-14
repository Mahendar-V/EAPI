using EAPI.DataAccess.DataAccess;
using EAPI.DataAccess.DataAccess.Interfaces;
using EAPI.DataModels;
using EAPI.DataModels.Exceptions;
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
    public class ProductServiceController : ControllerBase
    {
        private readonly IProductsDataAccess _dataAccess;
        public ProductServiceController(IProductsDataAccess dataAccess )
        {
            _dataAccess = dataAccess;
        }

        [HttpPost("add")]
        public IActionResult AddNewProduct([FromBody] ProductItem product)
        {
            try
            {
               return Ok(_dataAccess.AddProduct(product));
            }
            catch(DuplicateRecordError e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }
        [HttpGet("get")]
        public IActionResult GetProducts()
        {
            try
            {
                return Ok(_dataAccess.GetProducts());
            }
            
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }
        [HttpGet("delete/{pid:int}")]
        public IActionResult DeleteProduct(int pid)
        {
            try
            {
                return Ok(_dataAccess.DeleteProduct(pid));
            }
            catch(NotFoundError r)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { r.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

    }
}
