using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _productsService.GetAllAsync();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productsService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody] ProductDto product)
        {
            var productCreated = await _productsService.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            await _productsService.UpdateAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productsService.RemoveAsync(product);

            return NoContent();
        }
    }
}