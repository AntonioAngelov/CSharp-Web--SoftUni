namespace WebServer.Server
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using Handlers;
    using Http;
    using Http.Contracts;
    using Routing.Contracts;

    public class ConnectionHandler
    {
        private readonly Socket client;
        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            string request = await this.ReadRequest();

            if (string.IsNullOrEmpty(request) || string.IsNullOrWhiteSpace(request))
            {
                this.client.Shutdown(SocketShutdown.Both);
                return;
            }

            IHttpContext httpContext = new HttpContext(new HttpRequest(request));

            IHttpResponse response = new HttpHandler(this.serverRouteConfig)
                .Handle(httpContext);

            ArraySegment<byte> toBytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response.ToString()));

            await this.client.SendAsync(toBytes, SocketFlags.None);

            //if (response is ImageResponse && response.StatusCode == HttpStatusCode.OK)
            //{
            //    var responseAsImageResponse = response as ImageResponse;

            //    await this.client.SendAsync(responseAsImageResponse.Data, SocketFlags.None);
            //}

            Console.WriteLine("======REQUEST======");
            Console.WriteLine(request);
            Console.WriteLine("======Response======");
            Console.WriteLine(response.ToString());

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            StringBuilder request = new StringBuilder();
            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

            int numBytesRead;

            while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                request.Append(Encoding.UTF8.GetString(data.Array, 0, numBytesRead));
                
                if (numBytesRead < 1023)
                {
                    break;
                }
            }

            return request.ToString();
        }
    }
}
