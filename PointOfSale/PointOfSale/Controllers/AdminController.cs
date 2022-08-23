using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using PointOfSale.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(RoleManager<IdentityRole> roleManager,
                             UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [Authorize(Policy = "RoleShow")]
        public IActionResult Roles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [Authorize(Policy = "RoleAdd")]
        [HttpGet]
        public IActionResult AddNewRole()
        {
            var model = new RoleClaimViewModel
            {
                RoleName = ""
            };
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                RoleClaim roleClaim = new RoleClaim
                {
                    CliamType = claim.Type,
                    Add = false,
                    Edit = false,
                    Delete = false,
                    Show = false
                };
                model.Claims.Add(roleClaim);
            }
            return View(model);
        }

        [Authorize(Policy = "RoleAdd")]
        [HttpPost]
        public async Task<IActionResult> AddNewRole(RoleClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    var role = await roleManager.FindByNameAsync(model.RoleName);
                    var claims = await roleManager.GetClaimsAsync(role);

                    for (int i = 0; i < model.Claims.Count; i++)
                    {
                        var testresult = await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Show", model.Claims[i].Show ? "true" : "false"));
                        testresult = await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Add", model.Claims[i].Add ? "true" : "false"));
                        testresult = await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Edit", model.Claims[i].Edit ? "true" : "false"));
                        testresult = await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Delete", model.Claims[i].Delete ? "true" : "false"));
                    }
                    //if (testresult.Succeeded)
                    //{
                    //    return RedirectToAction("Permissions", "Users");
                    //}
                    return RedirectToAction("Roles", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Cannot Add Role");
                    return View(model);
                }

            }
            else
            {
                var models = new RoleClaimViewModel
                {
                    RoleName = ""
                };
                foreach (Claim claim in ClaimsStore.AllClaims)
                {
                    RoleClaim roleClaim = new RoleClaim
                    {
                        CliamType = claim.Type,
                        Add = false,
                        Edit = false,
                        Delete = false,
                        Show = false
                    };
                    models.Claims.Add(roleClaim);
                }
                return View(models);
            }
        }

        [HttpGet]
        [Authorize(Policy = "RoleEdit")]
        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id{roleId} cannot be found";
                return View("NotFound");
            }
           
            try
            {
                var existingRoleClaims = await roleManager.GetClaimsAsync(role);
                if (existingRoleClaims == null)
                {
                    ViewBag.ErrorMessage = $"Claims  cannot be found";
                    return View("NotFound");
                }

                EditRoleClaimViewModel model = new EditRoleClaimViewModel
                {
                    RoleName = role.Name,
                    Id = role.Id
                };
                foreach (Claim claim in ClaimsStore.AllClaims)
                {
                    RoleClaim roleClaim = new RoleClaim
                    {
                        CliamType = claim.Type
                    };
                    // check show 
                    if (existingRoleClaims.Any(c => c.Type == claim.Type + "Show" && c.Value == "true"))
                    {

                        roleClaim.Show = true;
                    }
                    else
                    {
                        roleClaim.Show = false;
                    }
                    // check Add
                    if (existingRoleClaims.Any(c => c.Type == claim.Type + "Add" && c.Value == "true"))
                    {

                        roleClaim.Add = true;
                    }
                    else
                    {
                        roleClaim.Add = false;
                    }
                    // check Edit
                    if (existingRoleClaims.Any(c => c.Type == claim.Type + "Edit" && c.Value == "true"))
                    {

                        roleClaim.Edit = true;
                    }
                    else
                    {
                        roleClaim.Edit = false;
                    }
                    // check Delete
                    if (existingRoleClaims.Any(c => c.Type == claim.Type + "Delete" && c.Value == "true"))
                    {

                        roleClaim.Delete = true;
                    }
                    else
                    {
                        roleClaim.Delete = false;
                    }
                    model.Claims.Add(roleClaim);
                }

                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = $"{role.Name} is has a Problem";
                ViewBag.ErrorMessage = $"{role.Name} role cannot be Edited";
                return View("Error");
            }

        }
       
        [HttpPost]
        [Authorize(Policy = "RoleEdit")]
        public async Task<IActionResult> EditRole(EditRoleClaimViewModel model, string roleid)
        {
            var role = await roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role cannot be found";
                return View("NotFound");
            }
            
                var existClaims = await roleManager.GetClaimsAsync(role);
            role.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                for (int i = 0; i < existClaims.Count; i++)
                {
                    var testresult = await roleManager.RemoveClaimAsync(role, new Claim(existClaims[i].Type, existClaims[i].Value));
                }
                for (int i = 0; i < model.Claims.Count; i++)
                {
                    await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Show", model.Claims[i].Show ? "true" : "false"));
                    await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Add", model.Claims[i].Add ? "true" : "false"));
                    await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Edit", model.Claims[i].Edit ? "true" : "false"));
                    await roleManager.AddClaimAsync(role, new Claim(model.Claims[i].CliamType + "Delete", model.Claims[i].Delete ? "true" : "false"));
                }
                var users = userManager.Users.ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    if (await userManager.IsInRoleAsync(users[i], role.Name))
                    {
                        var claims = await userManager.GetClaimsAsync(users[i]);
                        for (int k = 0; k < claims.Count; k++)
                        {
                            await userManager.RemoveClaimAsync(users[i], new Claim(claims[k].Type, claims[k].Value));
                        }
                        var AddClaims = await roleManager.GetClaimsAsync(role);
                        for (int k = 0; k < AddClaims.Count; k++)
                        {
                            await userManager.AddClaimAsync(users[i], new Claim(AddClaims[k].Type, AddClaims[k].Value));
                        }
                        //testresult = await roleManager.RemoveClaimAsync(role, new Claim(existClaims[i].Type + "", existClaims[i].Value));

                    }
                }


                return RedirectToAction("Roles");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Policy = "RoleDelete")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id{roleId} cannot be found";
                return View("NotFound");
            }

            try
            {
                var users = userManager.Users.ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    if (await userManager.IsInRoleAsync(users[i], role.Name))
                    {
                        ViewBag.ErrorTitle = "Cannot Delete Role";
                        ViewBag.ErrorMessage = $"There is {users[i].UserName} in Use";
                        ViewBag.returnUrl = Request.Headers["Referer"].ToString();
                        return View("Error");
                    }
                }

                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }
                return View("Roles");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = $"{role.Name} is in use";

                ViewBag.ErrorMessage = $"{role.Name} {ex.Message} role cannot be deleted  as there are users us this role" +
                    $"if you want to delete this role Remove users from this role";
                return View("Error");
            }


        }



        // Users Functions//////////////////////////////////////////////////////////////
        [Authorize(Policy = "UsersShow")]
        public async Task<IActionResult> Users()
        {
            List<ListUserRolesViewModel> model = new List<ListUserRolesViewModel>();
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < roles.Count; j++)
                {
                    if (await userManager.IsInRoleAsync(users[i], roles[j].Name))
                    {
                        ListUserRolesViewModel test = new ListUserRolesViewModel
                        {
                            RoleName = roles[j].Name,
                            UserId = users[i].Id,
                            UserName = users[i].UserName,
                            IsActive = users[i].IsActive
                            
                        };
                        model.Add(test);

                    }
                }
            }
            return View(model);
        }

        
        [HttpGet]
        [Authorize(Policy = "UsersAdd")]
        public IActionResult AddUser()
        {
            var roles = roleManager.Roles.ToList();
            List<Role> allroles = new List<Role>();
            for (int i = 0; i < roles.Count(); i++)
            {
                Role role = new Role
                {
                    Id = roles[i].Id,
                    Name = roles[i].Name
                };
                allroles.Add(role);
            }
            AllRolesViewModel model = new AllRolesViewModel
            {
                allRoles = allroles
            };
            return View(model);
        }
        public List<Role> RolesList()
        {
            var roless = roleManager.Roles.ToList();
            List<Role> allroless = new List<Role>();
            for (int i = 0; i < roless.Count(); i++)
            {
                Role role = new Role
                {
                    Id = roless[i].Id,
                    Name = roless[i].Name
                };
                allroless.Add(role);
            }
            return allroless;
        } 

        [HttpPost]
        [Authorize(Policy = "UsersAdd")]
        public async Task<IActionResult> AddUser(AllRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                // var emailExist = userManager.FindByEmailAsync(NormalizeKey(model.addUserViewModel.Email));
                var emailExist = userManager.Users.FirstOrDefault(x => x.NormalizedEmail == model.addUserViewModel.Email);
                if (emailExist != null)
                {
                    ModelState.AddModelError(string.Empty, "this Email is Exist error is ");
                    
                    AllRolesViewModel modelss = new AllRolesViewModel
                    {
                        allRoles = RolesList(),
                        addUserViewModel = model.addUserViewModel
                    };

                    return View(modelss);
                }
                var roleInDb = roleManager.Roles.FirstOrDefault(c => c.Id == model.addUserViewModel.RoleId);
                if(roleInDb.Name == "Admin")
                {
                    ModelState.AddModelError(string.Empty, "Can not Add Role Admin To Any One ");
                   
                    AllRolesViewModel modelss = new AllRolesViewModel
                    {
                        allRoles = RolesList(),
                        addUserViewModel = model.addUserViewModel
                    };

                    return View(modelss);
                }
                var user = new ApplicationUser
                {
                    UserName = model.addUserViewModel.Email,
                    Email = model.addUserViewModel.Email,
                    FullName = model.addUserViewModel.Name,
                    IsActive = false
                };
                var resuult = await userManager.CreateAsync(user, model.addUserViewModel.Password);
                if (resuult.Succeeded)
                {
                    var role = await roleManager.FindByIdAsync(model.addUserViewModel.RoleId);
                    if (role == null)
                    {
                        ModelState.AddModelError("", "Role is not exist");
                        return View(model);
                    }
                    await userManager.AddToRoleAsync(user, role.Name);
                    var claims = await roleManager.GetClaimsAsync(role);
                    for (int i = 0; i < claims.Count; i++)
                    {
                        await userManager.AddClaimAsync(user, new Claim(claims[i].Type, claims[i].Value));
                    }
                    return RedirectToAction("Users");
                }

                foreach (var error in resuult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            var roles = roleManager.Roles.ToList();

            List<Role> allroles = new List<Role>();
            for (int i = 0; i < roles.Count(); i++)
            {
                Role role = new Role
                {
                    Id = roles[i].Id,
                    Name = roles[i].Name
                };
                allroles.Add(role);
            }
            AllRolesViewModel models = new AllRolesViewModel
            {
                allRoles = allroles,
                addUserViewModel = model.addUserViewModel
            };

            return View(models);
        }

        [HttpPost]
        [Authorize(Policy = "UsersEdit")]
        public async Task<IActionResult> IsActive(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = !user.IsActive;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Users");
        }
        [HttpGet]
        [Authorize(Policy = "UsersEdit")]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ModelState.AddModelError("", "User Not Exist");
                return View("NotFound");
            }
            var roles = roleManager.Roles.ToList();
            var roleId = "";
            List<Role> allroles = new List<Role>();
            for (int i = 0; i < roles.Count(); i++)
            {
                Role role = new Role
                {
                    Id = roles[i].Id,
                    Name = roles[i].Name
                };
                allroles.Add(role);
                if (await userManager.IsInRoleAsync(user, roles[i].Name))
                {
                    roleId = roles[i].Id;
                }
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Name = user.FullName,
                Email = user.Email,
                UserId = user.Id,
                RoleId = roleId,
                Roles = allroles

            };

            return View(model);

        }

        [HttpPost]
        [Authorize(Policy = "UsersEdit")]
        public async Task<IActionResult> EditUser(EditUserViewModel model, string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return View("NotFound");
                }
                var users = userManager.Users.ToList();
                bool flage = false;
                for (int i = 0; i < users.Count(); i++)
                {
                    if (users[i].Email == model.Email && users[i].Id != user.Id)
                    {
                        flage = true;
                        break;
                    }
                }
                
                if (flage == true)
                {
                    ModelState.AddModelError(string.Empty, "this Email is Exist");
                    return View(model);
                }
                var roleInDb = roleManager.Roles.FirstOrDefault(c => c.Id == model.RoleId);
                if (roleInDb.Name == "Admin")
                {
                    ModelState.AddModelError(string.Empty, "Can not Add Role Admin To Any One ");

                    EditUserViewModel modelss = new EditUserViewModel
                    {
                        Name = model.Name,
                        Email = model.Email,
                        UserId = model.UserId,
                        RoleId = model.RoleId,
                        Roles = RolesList()

                    };

                    return View(modelss);
                }
                user.FullName = model.Name;
                user.Email = model.Email;
                user.UserName = model.Email;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var role = await roleManager.FindByIdAsync(model.RoleId);
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        var roles = roleManager.Roles.ToList();
                        for (int i = 0; i < roles.Count; i++)
                        {
                            if (await userManager.IsInRoleAsync(user, roles[i].Name))
                            {
                                var claims = await roleManager.GetClaimsAsync(roles[i]);
                                for (int k = 0; k < claims.Count; k++)
                                {
                                    await userManager.RemoveClaimAsync(user, new Claim(claims[k].Type, claims[k].Value));
                                }
                                await userManager.RemoveFromRoleAsync(user, roles[i].Name);

                            }
                        }

                        result = await userManager.AddToRoleAsync(user, role.Name);
                        var claimstouser = await roleManager.GetClaimsAsync(role);
                        for (int k = 0; k < claimstouser.Count; k++)
                        {
                            await userManager.AddClaimAsync(user, new Claim(claimstouser[k].Type, claimstouser[k].Value));
                        }
                        return RedirectToAction("Users");

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            var roleList = roleManager.Roles.ToList();
            List<Role> allroles = new List<Role>();
            for (int i = 0; i < roleList.Count(); i++)
            {
                Role rolee = new Role
                {
                    Id = roleList[i].Id,
                    Name = roleList[i].Name
                };
                allroles.Add(rolee);

            }
            EditUserViewModel models = new EditUserViewModel
            {
                Name = model.Name,
                Email = model.Email,
                UserId = model.UserId,
                RoleId = model.RoleId,
                Roles = allroles

            };
            return View(models);
        }

        [HttpPost]
        [Authorize(Policy = "UsersDelete")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = "User Not Found";
                return View("NotFound");
            }
            var claims = await userManager.GetClaimsAsync(user);
            for (int k = 0; k < claims.Count; k++)
            {
                await userManager.RemoveClaimAsync(user, new Claim(claims[k].Type, claims[k].Value));
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users", "Admin");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }
            return View("Users");
        }

    }
}
