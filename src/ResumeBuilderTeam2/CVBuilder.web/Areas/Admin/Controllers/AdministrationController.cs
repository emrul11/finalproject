using Autofac;
using Crud.Persistance.Features.Membership;
using CVBuilder.Domain;
using CVBuilder.Web.Areas.Admin.Models;
using CVBuilder.Web.Controllers;
using CVBuilder.Web.Models;
using CVBuilder.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace CVBuilder.Web.Areas.Admin.Controllers
{
    [Area("Admin")]


    //[Authorize]

    public class AdministrationController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AdministrationController> _logger;
        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        public AdministrationController(ILifetimeScope scope, ILogger<AdministrationController> logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _scope = scope;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //AccessDenied

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [Authorize]
        public IActionResult ListOfUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> ManageClaim(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userClaims = await _userManager.GetClaimsAsync(user);


            var model = new ManageClaimModel
            {
                Id = Convert.ToString(user.Id),
                FirstName = user.FirstName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Claims = userClaims.Select(c => c.Type).ToList()

            };


            return View(model);
        }

        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> AssignClaim(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var existingAssignClaim = await _userManager.GetClaimsAsync(user);

            var model = new AssignClaimModel
            {
                UserId = userId
            };

            foreach (var claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type,
                };
                if (existingAssignClaim.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                model.Claims.Add(userClaim);
            }

            return View(model);
        }
        [Authorize(Policy = "SuperAdminPolicy")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignClaim(AssignClaimModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }
            // Get all the user existing claims and delete them
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }
            // Add all the claims that are selected on the UI
            result = await _userManager.AddClaimsAsync(user,
                model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("ManageClaim", new { Id = model.UserId });
        }



        [Authorize(Policy = "SuperAdminPolicy")]

        public async Task<IActionResult> CreateRole()
        {
            var model = _scope.Resolve<RoleCreateModel>();
            return View(model);
        }
        [Authorize(Policy = "SuperAdminPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleCreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ResolveDependency(_scope);
                    var result = await model.CreateRole();

                    if (result.Succeeded)
                    {
                        TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                        {
                            Message = "Successfully added a new role.",
                            Type = ResponseTypes.Success
                        });
                        return RedirectToAction("ListOfRoles", "Administration");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
           
            catch (Exception e)
            {
                _logger.LogError(e, "Server Error");
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Insert role failed.",
                    Type = ResponseTypes.Danger
                });
            }

            return View();
        }

        [Authorize]

        public IActionResult ListOfRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> EditRole(string id)
        {
            var model = _scope.Resolve<RoleEditModel>();
            await model.LoadRole(id);
            return View(model);
        }
        [Authorize(Policy = "SuperAdminPolicy")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleEditModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                await model.EditRoleAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully updated role.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(ListOfRoles));
            }
           
            catch (Exception e)
            {
                // Handle other exceptions.
                _logger.LogError(e, "Server Error");
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Updating the role failed.",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction(nameof(ListOfRoles)); // Redirect back to the list of roles.
        }

        [Authorize(Policy = "SuperAdminPolicy")]

        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _scope.Resolve<RoleDeleteModel>();
                    await model.DeleteRoleAsync(id);

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully deleted role.",
                        Type = ResponseTypes.Success
                    });
                }
                else
                {
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Invalid ModelState. Role deletion failed.",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            
            catch (Exception e)
            {
                // Handle other exceptions.
                _logger.LogError(e, "Server Error");
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem deleting the role.",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction(nameof(ListOfRoles)); // Redirect back to the list of roles.
        }

        [Authorize(Policy = "SuperAdminPolicy")]

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {

            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleModel = new UserRoleModel
                {
                    UserId = Convert.ToString(user.Id),
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }

                model.Add(userRoleModel);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
