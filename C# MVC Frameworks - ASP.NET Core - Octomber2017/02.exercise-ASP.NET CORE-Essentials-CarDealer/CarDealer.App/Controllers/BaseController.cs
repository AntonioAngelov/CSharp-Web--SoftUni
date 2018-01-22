namespace CarDealer.App.Controllers
{
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class BaseController : Controller
    {
        protected readonly ILogService logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BaseController(ILogService logger, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected string GetCurrentUserId() => this.httpContextAccessor
            .HttpContext
            .User
            .FindFirst(ClaimTypes.NameIdentifier)
            .Value;
    }
}
