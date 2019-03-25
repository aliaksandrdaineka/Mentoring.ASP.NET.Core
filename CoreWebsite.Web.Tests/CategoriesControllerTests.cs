using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Web.Controllers;
using CoreWebsite.Web.Mapping.Interfaces;
using CoreWebsite.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebsite.Web.Tests
{
    [TestClass]
    public class CategoriesControllerTests
    {
        private Mock<ICategoriesService> _categoriesServiceMock;
        private Mock<ICategoryViewModelMapper> _categoryViewModelMapperMock;
        private CategoriesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _categoriesServiceMock = new Mock<ICategoriesService>();
            _categoryViewModelMapperMock = new Mock<ICategoryViewModelMapper>();
            _controller = new CategoriesController(_categoriesServiceMock.Object, _categoryViewModelMapperMock.Object);
        }

        [TestMethod]
        public async Task Index_ShouldReturnViewModels()
        {
            _categoriesServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<CategoryDto>(
                new List<CategoryDto>() {
                    new CategoryDto { CategoryId = 1, CategoryName = "Liquids"},
                    new CategoryDto { CategoryId = 2, CategoryName = "Wood"},
                    new CategoryDto { CategoryId = 3, CategoryName = "Food"}
                }));

            _categoryViewModelMapperMock.Setup(x => x.MapToViewModel(It.IsAny<CategoryDto>()))
                .Returns((CategoryDto x) => new CategoryViewModel()
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName
                });

            var result = await _controller.Index() as ViewResult;
            var model = result.ViewData.Model as IEnumerable<CategoryViewModel>;

            Assert.IsNotNull(model);
            Assert.AreEqual(3, model.Count());
        }
    }

}
