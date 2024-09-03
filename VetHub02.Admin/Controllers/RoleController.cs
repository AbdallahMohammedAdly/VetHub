using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetHub02.Admin.Models;


namespace VetHub02.Admin.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Roles = await roleManager.Roles.ToListAsync();

            return View(Roles);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExists = await roleManager.RoleExistsAsync(model.Name);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = model.Name.Trim() });

                    return RedirectToAction(nameof(Index), await roleManager.Roles.ToListAsync());
                }
                else
                {
                    ModelState.AddModelError("Name", "Role is already existed!");

                    return View(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            await roleManager.DeleteAsync(role);

            return RedirectToAction(nameof(Index));
        
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var mappedRole = new RoleViewModel()
            {
                Name = role.Name,
            };


            return View(mappedRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id,RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roleExist = await roleManager.RoleExistsAsync(model.Name);

                if (!roleExist)
                {
                    var role = await roleManager.FindByIdAsync(model.Id);
                    role.Name = model.Name;
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role is already existed!");

                    return View(nameof(Index));
                }
                
            }



            return RedirectToAction(nameof(Index));
        }

    }
}
