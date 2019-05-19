using CoreWebsite.Web.Infrastructure;
using CoreWebsite.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CoreWebsite.Web.Components
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        public List<BreadcrumbsViewModel> Breadcrumbs { get; set; }
        private readonly string _defaultControllerName;
        private readonly string _defaultActionName;

        public BreadcrumbsViewComponent()
        {
            _defaultControllerName = "Home";
            _defaultActionName = "Index";
        }

        public IViewComponentResult Invoke()
        {
            Breadcrumbs = new List<BreadcrumbsViewModel>
            {
                new BreadcrumbsViewModel(_defaultControllerName, _defaultActionName, _defaultControllerName)
            };

            var action = ViewContext.ActionDescriptor as ControllerActionDescriptor;

            if (action == null)
                return View(Breadcrumbs);

            if (!action.ControllerName.Equals(_defaultControllerName, StringComparison.InvariantCultureIgnoreCase))
                Breadcrumbs.Add(new BreadcrumbsViewModel(action.ControllerName, _defaultActionName, action.ControllerName));

            var currentPageTitle = ViewData["Title"] != null ? ViewData["Title"].ToString() : action.ActionName;

            if (!action.ActionName.Equals(_defaultActionName, StringComparison.InvariantCultureIgnoreCase))
                Breadcrumbs.Add(new BreadcrumbsViewModel(currentPageTitle, action.ActionName, action.ControllerName));

            return View(Breadcrumbs);
        }
    }
}
