namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (var writer = new StreamWriter("log.txt", true))
            {
                var date = DateTime.UtcNow;
                var ipAdress = context.HttpContext.Connection.RemoteIpAddress;
                var username = context.HttpContext.User.Identity.Name ?? "Anonymous";
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                var logMessage = $"{date} - {ipAdress} - {username} - {controller}.{action}";

                if (context.Exception != null)
                {
                    var exceptionType = context.Exception.GetType().Name;
                    var exceptiomMessage = context.Exception.Message;

                    logMessage = $"[!] {logMessage} - {exceptionType} - {exceptiomMessage}";
                }

                writer.WriteLine(logMessage);
            }
        }
    }
}
