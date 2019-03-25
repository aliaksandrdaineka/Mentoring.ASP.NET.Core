using CoreWebsite.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebsite.Web.Filters
{
    public class ActionLoggingFilter : IAsyncActionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ISettingsProvider _settingsProvider;

        private bool IsParametersLoggingEnabled { get; }

        public ActionLoggingFilter(ILoggerFactory loggerFactory, ISettingsProvider settings)
        {
            _loggerFactory = loggerFactory;
            IsParametersLoggingEnabled = settings.IsParametersLoggingEnabled;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = _loggerFactory.CreateLogger(context.Controller.GetType());

            logger.LogInformation("Action started");

            if (IsParametersLoggingEnabled)


            await next();

            logger.LogInformation("Action finished");
        }

        private void LogParameters(ActionExecutingContext context, ILogger logger)
        {
            if (!context.ActionArguments.Any())
            {
                logger.LogInformation("Action has no parameters.");
                return;
            }

            var parametersText = new StringBuilder("Action parameters:\n");
            foreach(var parameter in context.ActionArguments)
            {
                parametersText.Append($"{parameter.Key}:\t{parameter.Value}\n");
            }

            logger.LogInformation(parametersText.ToString());
        }
    }
}
