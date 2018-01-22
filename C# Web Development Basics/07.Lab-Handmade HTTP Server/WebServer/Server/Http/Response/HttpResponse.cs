namespace WebServer.Server.Http.Response
{
    using Enums;
    using System.Text;
    using Contracts;

    public abstract class HttpResponse : IHttpResponse
    {
        private string StatusCodeMessage => this.StatusCode.ToString();

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public HttpStatusCode StatusCode { get; protected set; }

        public void AddHeader(string key, string value)
        {
            HttpHeader header = new HttpHeader(key, value);
            this.Headers.Add(header);
        }

        public override string ToString()
        {
            var response = new StringBuilder();

            response.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCodeMessage}");
            response.AppendLine(this.Headers.ToString());
            
            return response.ToString();
        }
    }
}
