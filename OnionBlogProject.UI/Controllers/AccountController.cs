using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBlogProject.Application.Models.Dtos.UserDto;
using OnionBlogProject.Application.Services.AppUserService;

namespace OnionBlogProject.UI.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _userService;

        public AccountController(IAppUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            var result = await _userService.Register(registerDto);
            return RedirectToAction("Index");

        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost,AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(loginDto);

                if(result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("Hata", "Hatalı Giriş İşlemi");
            }
            return View(loginDto);
        }

        private IActionResult RedirectToLocal(string returnUrl = "/")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        public async Task<IActionResult> Edit(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDto updateProfileDto)
        {
            if(ModelState.IsValid)
            {
                await _userService.UpdateUser(updateProfileDto);
                TempData["Success"] = "Güncelleme İşlemi Başarıyla Gerçekleşti.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Güncelleme İşlemi Başarısız.";
                return View(updateProfileDto);
            }

        }

        public IActionResult LogOut()
        {
            _userService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
