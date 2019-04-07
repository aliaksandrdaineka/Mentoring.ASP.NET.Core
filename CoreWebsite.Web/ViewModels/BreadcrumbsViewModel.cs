using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebsite.Web.ViewModels
{
    public class BreadcrumbsViewModel
    {
        public BreadcrumbsViewModel(string name, string action, string controller)
        {
            ActionName = action;
            ControllerName = controller;
            DisplayName = name;
        }

        public string DisplayName { get; }
        public string ActionName { get; }
        public string ControllerName { get; }
    }
}
