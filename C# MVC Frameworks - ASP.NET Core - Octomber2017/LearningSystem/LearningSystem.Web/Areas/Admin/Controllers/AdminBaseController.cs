namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole)]
    public abstract class AdminBaseController : Controller
    {
    }
}
