using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Api.Controllers
{
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

    }
}
