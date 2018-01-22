namespace _03.SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            int port = 1300;
            TcpListener listener = new TcpListener(address, port);
            listener.Start();

            Console.WriteLine($"Server started.");
            Console.WriteLine($"Listening to TCP client at 127.0.0.1:{port}");

            Task.Run(() => ConnectAsync(listener))
                .GetAwaiter()
                .GetResult();
        }

        private static async Task ConnectAsync(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waitig for client...");

                using (TcpClient client = await listener.AcceptTcpClientAsync())
                {
                    Console.WriteLine("Client connected.");

                    byte[] buffer = new byte[1024];
                    await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                    var message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(message.Trim('\0'));

                    string response = "HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello from local server.";
                    var writeBuffer = Encoding.UTF8.GetBytes(response);
                    await client.GetStream().WriteAsync(writeBuffer, 0, writeBuffer.Length);

                    Console.WriteLine("Closing connection.");

                }
            }
        }
    }
}
