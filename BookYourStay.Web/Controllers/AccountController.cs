using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Application.Common.Utilities;
using BookYourStay.Domain.Entities;
using BookYourStay.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookYourStay.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new()
            {
                ReturnUrl = returnUrl
            };

            return View(loginVM);
        }
        public IActionResult Register()
        {
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            }

            RegisterVM registerVM = new()
            {
                RoleList = _roleManager.Roles.Select(r =>
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Name
                    })
            };

            return View(registerVM);
        }
    }
}
