namespace WebServer.Server.Http.Response
{
    using Enums;
    using Common;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }
    }
}
