using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Api.Models;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categories = await _categoriesService.GetAllAsync();

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> Get(int id)
        {
            var image = await _categoriesService.GetPictureAsync(id);

            if (image == null)
                return NotFound();

            return Ok(image);
        }
        
        [HttpPatch("{id}/image")]
        public async Task<IActionResult> UpdateImage(int id, [FromBody] CategoryImageUpload image)
        {
            if (image.File.Length == 0 || image.File.ContentType != "image/bmp")
                return BadRequest();

            using (var memoryStream = new MemoryStream())
            {
                await image.File.CopyToAsync(memoryStream);
                await _categoriesService.UpdatePictureAsync(id, memoryStream.ToArray());
            }

            return NoContent();
        }

    }
}
