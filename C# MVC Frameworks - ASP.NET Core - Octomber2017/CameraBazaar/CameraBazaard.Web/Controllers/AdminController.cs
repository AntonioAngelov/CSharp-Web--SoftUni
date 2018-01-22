namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services;
    using CameraBazaar.Web.Models.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService admins;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public AdminController(
            IAdminService admins,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.admins = admins;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Users()
            => View(admins.AllUsers());

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var currentUser = this.admins.UserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            currentUser.Roles = roles;

            return View(currentUser);
        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(new SetRoleViewModel
            {
                UserId = id,
                Roles = this.GetRolesSelectItems()
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await this.userManager.GetRolesAsync(user);
            await this.userManager.RemoveFromRolesAsync(user, userRoles);
            await this.userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> AddRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(new SetRoleViewModel
            {
                UserId = id,
                Roles = this.GetRolesSelectItems()
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> RemoveRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            await this.userManager.RemoveFromRolesAsync(user, roles);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        private IEnumerable<SelectListItem> GetRolesSelectItems()
            => this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();
    }
}
