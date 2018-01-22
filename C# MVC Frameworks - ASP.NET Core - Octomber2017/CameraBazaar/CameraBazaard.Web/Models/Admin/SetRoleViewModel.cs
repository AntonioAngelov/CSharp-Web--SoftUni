namespace CameraBazaar.Web.Models.Admin
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class SetRoleViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
