using System;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWebsite.Data.Models;
using CoreWebsite.Web.Mapping.Interfaces;
using CoreWebsite.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreWebsite.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ISuppliersService _suppliersService;
        private readonly ILogger _logger;
        private readonly IProductViewModelMapper _productMapper;


        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, ISuppliersService suppliersService, ILogger<ProductsController> logger, IProductViewModelMapper productMapper)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _suppliersService = suppliersService;
            _logger = logger;
            _productMapper = productMapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _productsService.SearchAsync();

            var productsViewModels = products.Select(x => _productMapper.MapToViewModel(x));

            return View(productsViewModels);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Getting product with ID = {id}");
            var product = await _productsService.GetByIdAsync(id.Value);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID = {id} is not found");
                return NotFound();
            }

            return View(_productMapper.MapToViewModel(product));
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = _productMapper.MapToDto(viewModel);
                await _productsService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName", viewModel.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName", viewModel.SupplierId);

            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = _productMapper.MapToViewModel(product);

            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName", product.SupplierId);
            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel viewModel)
        {
            if (id != viewModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.UpdateAsync(_productMapper.MapToDto(viewModel));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogWarning(ex.Message);
                    if (!ProductExists(viewModel.ProductId).Result)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName", viewModel.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName", viewModel.SupplierId);
            return View(viewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(_productMapper.MapToViewModel(product));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            await _productsService.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            var result = await _productsService.FindAsync(e => e.ProductId == id);
            return result.Any();
        }
    }
}
