using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWebsite.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreWebsite.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ISuppliersService _suppliersService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;


        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, ISuppliersService suppliersService, IConfiguration configuration, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _suppliersService = suppliersService;
            _configuration = configuration;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productsService.SearchAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Getting product with ID = {id}");
            var product = await _productsService.FindAsync(id.Value);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID = {id} is not found");
                return NotFound();
            }

            return View(product);
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
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.FindAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogWarning(ex.Message);
                    if (!ProductExists(product.ProductId).Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAllAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _suppliersService.GetAllAsync(), "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService
                .FindAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsService.FindAsync(id);
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
