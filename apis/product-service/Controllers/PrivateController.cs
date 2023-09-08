using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using product_service.Interfaces;

namespace product_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateController : ControllerBase
    {
        
        private readonly IProduct _productReposity;
        
        public PrivateController(IProduct productReposity)
        {
            _productReposity = productReposity;
        }
        
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            Console.WriteLine("Test function is called");
            return Ok("this message is from product service");
        }

        [HttpGet]
        [Route("CheckProductExist/{productId}")]
        public async Task<IActionResult> CheckProductExist(int productId)
        {
            var product = await _productReposity.GetProductById(productId);
            if (product == null)
            {
                return Ok(false);
            }
            else
            {
                return Ok(true);
            }
        }
        
    }
}
