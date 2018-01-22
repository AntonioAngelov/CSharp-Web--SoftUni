namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.Admin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : AdminBaseController
    {
        private readonly IAdminService admins;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdminService admins,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.admins = admins;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult All()
           => View(admins.UserListing());

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var currentUser = this.admins.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            currentUser.Roles = roles;

            return View(currentUser);
        }

        public async Task<IActionResult> AddRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(new AddRoleViewModel
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

        public async Task<IActionResult> ClearRoles(string id)
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
