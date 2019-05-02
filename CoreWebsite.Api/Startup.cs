using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Mapping;
using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Services;
using CoreWebsite.Data;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;
using CoreWebsite.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreWebsite.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureRepositoryServices(services);
            ConfigureBllServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }

        private void ConfigureBllServices(IServiceCollection services)
        {
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ISuppliersService, SuppliersService>();
            services.AddTransient<ISettingsProvider, SettingsProvider>();

            services.AddTransient<IProductDtoMapper, ProductDtoMapper>();
            services.AddTransient<ICategoryDtoMapper, CategoryDtoMapper>();
            services.AddTransient<ISupplierDtoMapper, SupplierDtoMapper>();
        }

        private void ConfigureRepositoryServices(IServiceCollection services)
        {
            services.AddTransient<IRepository<Product>, ProductsRepository>();
            services.AddTransient<IRepository<Category>, CategoriesRepository>();
            services.AddTransient<IRepository<Supplier>, SuppliersRepository>();
        }
    }
}
