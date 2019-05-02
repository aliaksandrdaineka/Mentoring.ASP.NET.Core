using CoreWebsite.BLL.Models.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreWebsite.Api.ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowCategories(List<CategoryDto> categories)
        {
            Console.WriteLine("Categories:");
            foreach(var category in categories)
            {
                Console.WriteLine($"{category.CategoryId}.\t{category.CategoryName}");
            }
           
        }

        static void ShowProducts(List<ProductDto> products)
        {
            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId}.\t{product.ProductName}\t{product.UnitPrice}\t{product.Category.CategoryName}");
            }
        }

        static async Task<List<ProductDto>> GetProductsAsync(string path)
        {
            List<ProductDto> products = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<List<ProductDto>>();
            }

            return products;
        }

        static async Task<List<CategoryDto>> GetCategoriesAsync(string path)
        {
            List<CategoryDto> categories = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<List<CategoryDto>>();
            }

            return categories;
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:57177/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var products = await GetProductsAsync("products");
                ShowProducts(products);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
