using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Web.Controllers;
using CoreWebsite.Web.Mapping.Interfaces;
using CoreWebsite.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWebsite.Web.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<IProductsService> _productsServiceMock;
        private Mock<ICategoriesService> _categoriesServiceMock;
        private Mock<ISuppliersService> _suppliersServiceMock;
        private Mock<ILogger<ProductsController>> _loggerMock;
        private Mock<IProductViewModelMapper> _productViewModelMapperMock;

        private ProductsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _productsServiceMock = new Mock<IProductsService>();
            _productViewModelMapperMock = new Mock<IProductViewModelMapper>();
            _categoriesServiceMock = new Mock<ICategoriesService>();
            _suppliersServiceMock = new Mock<ISuppliersService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();

            _controller = new ProductsController(
                _productsServiceMock.Object,
                _categoriesServiceMock.Object, 
                _suppliersServiceMock.Object,
                _loggerMock.Object, 
                _productViewModelMapperMock.Object);
        }

        [TestMethod]
        public async Task Create_ValidModel_ShouldCreateProduct()
        {
            var viewModel = new ProductViewModel() { ProductName = "New product"};
            await _controller.Create(viewModel);

            _productsServiceMock.Verify(x => x.CreateAsync(It.IsAny<ProductDto>()), Times.Once);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ShouldNotCreateProduct()
        {
            var viewModel = new ProductViewModel() { ProductName = "" };
            _controller.ModelState.AddModelError("ProductName", "Required");
            await _controller.Create(viewModel);

            _productsServiceMock.Verify(x => x.CreateAsync(It.IsAny<ProductDto>()), Times.Never);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ShouldStaySamePage()
        {
            var productNameToTest = "ProductNameToTest";
            var product = new ProductViewModel() { ProductName = productNameToTest };
            _controller.ModelState.AddModelError("TestError", "Required");
            var viewResult = await _controller.Create(product) as ViewResult;

            var viewModel = viewResult.ViewData.Model as ProductViewModel;

            Assert.AreEqual(productNameToTest, viewModel.ProductName);
            // Assert.AreEqual(nameof(_controller.Create), viewResult.ViewName);
        }

        [TestMethod]
        public async Task Index_ShouldReturnProducts()
        {
            await _controller.Index();
            _productsServiceMock.Verify(x => x.SearchAsync(null), Times.Once);
        }

        [TestMethod]
        public async Task Edit_ValidModel_ShouldPerformEdit()
        {
            var viewModel = new ProductViewModel() { ProductId = 1, ProductName = "New Name"};
            var result = await _controller.Edit(viewModel.ProductId, viewModel) as RedirectToActionResult;

            _productsServiceMock.Verify(x => x.UpdateAsync(It.IsAny<ProductDto>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(_controller.Index), result.ActionName);
        }

        [TestMethod]
        public async Task Edit_InvalidModel_ShouldStaySamePage()
        {
            _controller.ModelState.AddModelError("ValidationError", "Error message");
            var result = await _controller.Edit(1, new ProductViewModel() { ProductId = 1 }) as ViewResult;
            var viewModel = result.ViewData.Model as ProductViewModel;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, viewModel.ProductId);
        }

        [TestMethod]
        public async Task Edit_ViewGetEditValidId_ShouldReturnEditView()
        {
            var dto = new ProductDto
            {
                ProductId = 5,
                ProductName = "Test"
            };
            _productsServiceMock.Setup(x => x.GetByIdAsync(dto.ProductId)).ReturnsAsync(dto);
            _productViewModelMapperMock.Setup(x => x.MapToViewModel(dto)).Returns(
                new ProductViewModel
                {
                    ProductId = 5,
                    ProductName = "Test"
                });

            var result = await _controller.Edit(dto.ProductId) as ViewResult;
            var viewModel = result.ViewData.Model as ProductViewModel;

            Assert.IsNotNull(result);
            Assert.AreEqual(dto.ProductName, viewModel.ProductName);
        }
    }
}
