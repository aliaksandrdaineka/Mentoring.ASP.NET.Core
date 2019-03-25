using CoreWebsite.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebsite.Web.Filters
{
    public class ActionLoggingFilterFactory : IFilterFactory
    {
        public bool IsReusable { get; }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new ActionLoggingFilter(serviceProvider.GetService<ILoggerFactory>(), serviceProvider.GetService<ISettingsProvider>());
        }
    }
}
