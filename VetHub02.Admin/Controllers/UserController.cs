using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VetHub02.Admin.Models;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.UnitOfWork;

namespace VetHub02.Admin.Controllers
{
   
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                DisplayName = u.DisplayName,
                PhoneNumber = u.PhoneNumber,
                Roles = _userManager.GetRolesAsync(u).Result
            }).ToList();

            return View(userViewModels);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var allRoles = await _roleManager.Roles.ToListAsync();
                var userRoles = await _userManager.GetRolesAsync(user);
                var viewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = allRoles.Select(r => new RoleViewModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        IsSelected = userRoles.Contains(r.Name)
                    }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while editing user with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound($"User with ID {model.UserId} not found.");
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var rolesToAdd = model.Roles.Where(r => r.IsSelected && !userRoles.Contains(r.Name)).Select(r => r.Name);
                var rolesToRemove = userRoles.Where(r => !model.Roles.Any(m => m.Name == r && m.IsSelected));

                await _userManager.AddToRolesAsync(user, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating roles for user with ID {model.UserId}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var viewModel = new UserUpdateViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    PhoneNumber = user.PhoneNumber
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving user data for update with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound($"User with ID {model.Id} not found.");
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.DisplayName = model.DisplayName;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                                     
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return StatusCode(500, $"Failed to update user with ID {model.Id}. Please try again later.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating user with ID {model.Id}.");
                return StatusCode(500, "Internal server error");
            }
        }

      
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return StatusCode(500, $"Failed to delete user with ID {id}. Please try again later.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting user with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
