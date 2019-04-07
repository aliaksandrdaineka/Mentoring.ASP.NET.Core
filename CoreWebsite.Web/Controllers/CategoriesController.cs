using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.Web.Mapping.Interfaces;
using CoreWebsite.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly ICategoryViewModelMapper _categoryViewModelMapper;
        private const string _categoryImageContentType = "image/bmp";

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

        public async Task<IActionResult> GetPicture(int id)
        {
            var picture = await _categoriesService.GetPictureAsync(id);
            var stream = new MemoryStream(picture);

            return new FileStreamResult(stream, _categoryImageContentType);
        }

        public async Task<IActionResult> DownloadPicture(int id)
        {
            var picture = await _categoriesService.GetPictureAsync(id);
            return File(picture, _categoryImageContentType, $"category_{id}.bmp");
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = _categoryViewModelMapper.MapToViewModel(category);

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = _categoryViewModelMapper.MapToViewModel(category);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (id != model.CategoryId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Picture != null)
            {
                using (var stream = new MemoryStream())
                {
                    await model.Picture.CopyToAsync(stream);
                    await _categoriesService.UpdatePictureAsync(model.CategoryId, stream.ToArray());
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}