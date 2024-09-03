using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Entities.identity;
using VetHub02.DTO;

namespace VetHub02.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AdminController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) 
            {
                ModelState.AddModelError("Email", "Invaild Email");
                return RedirectToAction(nameof(Login));
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password,true);

            if (!result.Succeeded )//|| !await userManager.IsInRoleAsync(user,"Admin")) 
            {
                ModelState.AddModelError(string.Empty, "You Are Not Authorized");
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return RedirectToAction("Index", "Home", new {data = user});
            }
            
        }


        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }





    }
}
