using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.Web.Mapping.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly ICategoryViewModelMapper _categoryViewModelMapper;

        public CategoriesController(ICategoriesService categoriesService, ICategoryViewModelMapper categoryViewModelMapper)
        {
            _categoriesService = categoriesService;
            _categoryViewModelMapper = categoryViewModelMapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAllAsync();
            return View(categories.Select(x => _categoryViewModelMapper.MapToViewModel(x)));
        }
    }
}