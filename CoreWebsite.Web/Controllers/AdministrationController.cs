using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Web.Areas.Identity.Data;
using CoreWebsite.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdministrationController : Controller
    {
        private readonly UserManager<CoreWebsiteUser> _userManager;

        public AdministrationController(UserManager<CoreWebsiteUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}