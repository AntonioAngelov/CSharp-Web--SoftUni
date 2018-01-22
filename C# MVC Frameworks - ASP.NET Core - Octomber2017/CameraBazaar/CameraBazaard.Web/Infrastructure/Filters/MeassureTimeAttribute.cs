namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class MeassureTimeAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public MeassureTimeAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopwatch.Stop();

            using (var writer = new StreamWriter("action-times.txt", true))
            {
                var date = DateTime.UtcNow;
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];
                var elapsedTime = this.stopwatch.Elapsed;

                var log = $"{date} - {controller}.{action} - {elapsedTime}";

                writer.WriteLine(log);
            }
        }

    }
}
