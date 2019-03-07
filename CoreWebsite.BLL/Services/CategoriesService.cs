using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoriesRepository.GetAllAsync();
        }
    }
}
