using System;
using CoreWebsite.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CoreWebsite.Web.Areas.Identity.IdentityHostingStartup))]
namespace CoreWebsite.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CoreWebsiteIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CoreWebsiteIdentityContextConnection")));

                services.AddIdentity<CoreWebsiteUser, IdentityRole>(config =>
                    {
                        //config.SignIn.RequireConfirmedEmail = true;
                    })
                    .AddEntityFrameworkStores<CoreWebsiteIdentityContext>()
                    .AddDefaultTokenProviders();

                //services.AddIdentity<CoreWebsiteUser, IdentityRole>(config =>
                //{
                //config.SignIn.RequireConfirmedEmail = true;
                //})
                // services.AddDefaultIdentity<IdentityUser>()
                // .AddEntityFrameworkStores<CoreWebsiteIdentityContext>()
                // .AddDefaultTokenProviders();
            });
        }
    }
}