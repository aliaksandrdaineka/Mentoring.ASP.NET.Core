using System.Text;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Mapping;
using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Services;
using CoreWebsite.Data;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;
using CoreWebsite.Data.Repositories;
using CoreWebsite.Web.Mapping;
using CoreWebsite.Web.Mapping.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreWebsite.Web
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureRepositoryServices(services);
            ConfigureBllServices(services);
            ConfigureWebServices(services);

            _logger.LogInformation("Registered services");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                var sb = new StringBuilder("Application started. Application configuration:\n");
              
                foreach (var conf in Configuration.AsEnumerable())
                {
                    sb.Append($"{conf.Key}:\t{conf.Value}\n");
                }
                _logger.LogInformation(sb.ToString());
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
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

        private void ConfigureWebServices(IServiceCollection services)
        {
            services.AddTransient<IProductViewModelMapper, ProductViewModelMapper>();
            services.AddTransient<ICategoryViewModelMapper, CategoryViewModelMapper>();
        }
    }
}
