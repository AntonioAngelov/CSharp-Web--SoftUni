using Microsoft.AspNetCore.Mvc;

namespace CarDealer.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Models.Logs;
    using Services;

    public class LogsController : Controller
    {
        private const int ListingPageSize = 20;

        private readonly ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }

        [Authorize]
        public IActionResult All(string search, int page = 1)
        {
            string trimedSearch = search?.Trim();

            var allLogs = this.logs
                .All(page, ListingPageSize, search!= null ? trimedSearch : null);

            return this.View(new AllLogsListingModel
            {
                Query = $"&search={trimedSearch}",
                CurrentPage = page,
                Logs = allLogs,
                TotalPages = (int) Math.Ceiling(this.logs.Total(trimedSearch) / (double) ListingPageSize),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Clear()
        {
            this.logs.Clear();

            return RedirectToAction(nameof(this.All));
        }

    }
}