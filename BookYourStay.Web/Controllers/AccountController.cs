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

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVm.Email,
                    loginVm.Password, loginVm.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVm.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return LocalRedirect(loginVm.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(loginVm);
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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVm)
        {
            ApplicationUser user = new()
            {
                UserName = registerVm.Email,
                Email = registerVm.Email,
                EmailConfirmed = true,
                NormalizedEmail = registerVm.Email.ToUpper(),
                Name = registerVm.Name,
                PhoneNumber = registerVm.PhoneNumber,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(registerVm.Role) &&
                    (registerVm.Role == SD.Role_Admin || registerVm.Role == SD.Role_Customer))
                {
                    await _userManager.AddToRoleAsync(user, registerVm.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (string.IsNullOrEmpty(registerVm.ReturnUrl))
                    return RedirectToAction("Index", "Home");

                return LocalRedirect(registerVm.ReturnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            registerVm = new()
            {
                RoleList = _roleManager.Roles.Select(r =>
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Name
                    })
            };

            return View(registerVm);
        }

        public IActionResult AccessDenied()
        {
            return View();
            //return View("~/Views/Account/AccessDenied.cshtml");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}