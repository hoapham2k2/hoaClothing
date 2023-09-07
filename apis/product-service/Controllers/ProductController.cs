using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using product_service.Dtos;
using product_service.Interfaces;

namespace product_service.Controllers
{
    [Route("/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productReposity;
        private readonly ICategory _categoryReposity;
        
        public ProductController(IProduct productReposity, ICategory categoryReposity)
        {
            _productReposity = productReposity;
            _categoryReposity = categoryReposity;
        }
        
        [HttpGet]
        [Authorize (Policy = "User")]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productReposity.GetProducts();
            return Ok(products);
        }
        
        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productReposity.GetProductById(id);
            return Ok(product);
        }
        
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryReposity.GetCategories();
            return Ok(categories);
        }
        
        [HttpGet]
        [Route("{categoryId}/products")]
        public async Task<IActionResult> GetProductByCategoryId(int categoryId)
        {
            var products = await _productReposity.GetProductByCategoryId(categoryId);
            return Ok(products);
        }
        
        //limit result
        [HttpGet]
        [Route("products/limit={limit}")]
        public async Task<IActionResult> GetProducts(int limit)
        {
            var products = await _productReposity.GetProducts(limit);
            return Ok(products);
        }
        
        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            var product = await _productReposity.CreateProduct(productCreateDto);
            return Ok(product);
        }
    }
}
