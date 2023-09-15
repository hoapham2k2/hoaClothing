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
        private readonly ILogger<ProductController> _logger;
        
        public ProductController(IProduct productReposity, ICategory categoryReposity, ILogger<ProductController> logger)
        {
            _productReposity = productReposity;
            _categoryReposity = categoryReposity;
            _logger = logger;
        }
        
        [HttpGet]
        // [Authorize (Policy = "User")]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("==================Requesting: Get All Products");
            var products = await _productReposity.GetProducts();
            return Ok(products);
        }
        
        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            _logger.LogInformation("==================Requesting: Get Product By Id");
            var product = await _productReposity.GetProductById(id);
            return Ok(product);
        }
        
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            _logger.LogInformation("==================Requesting: Get All Categories");
            var categories = await _categoryReposity.GetCategories();
            return Ok(categories);
        }
        
        
        [HttpGet]
        [Route("{categoryId}/products")]
        public async Task<IActionResult> GetProductByCategoryId(int categoryId)
        {
            _logger.LogInformation("==================Requesting: Get Product By Category Id");
            var products = await _productReposity.GetProductByCategoryId(categoryId);
            return Ok(products);
        }
        
        //get category by id
        [HttpGet]
        [Route("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            _logger.LogInformation("==================Requesting: Get Category By Id");
            var category = await _categoryReposity.GetCategoryById(id);
            return Ok(category);
        }
        
        //limit result
        [HttpGet]
        [Route("products/limit={limit}")]
        public async Task<IActionResult> GetProducts(int limit)
        {
            _logger.LogInformation("==================Requesting: Get Products");
            var products = await _productReposity.GetProducts(limit);
            return Ok(products);
        }
        
        //get random products
        [HttpGet]
        [Route("products/random/{limit}")]
        public async Task<IActionResult> GetRandomProducts(int limit)
        {
            _logger.LogInformation("==================Requesting: Get Random Products");
            var products = await _productReposity.GetRandomProducts(limit);
            return Ok(products);
        }
        
        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            _logger.LogInformation("==================Requesting: Create Product");
            var product = await _productReposity.CreateProduct(productCreateDto);
            return Ok(product);
        }
        
        [HttpPut]
        [Route("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            _logger.LogInformation("==================Requesting: Update Product");
            var product = await _productReposity.UpdateProduct(id, productUpdateDto);
            return Ok(product);
        }
        
        [HttpDelete]
        [Route("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation("==================Requesting: Delete Product");
            var product = await _productReposity.DeleteProduct(id);
            return Ok(product);
        }
    }
}
