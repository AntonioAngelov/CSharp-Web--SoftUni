namespace _02.ValidateUrl
{
    using System;
    using System.Net;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var inputUrl = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(inputUrl);

            try
            {
                var uri = new Uri(decodedUrl);

                var protocol = uri.Scheme;
                var host = uri.Host;
                var port = uri.Port;
                var path = uri.AbsolutePath;

                if (protocol == string.Empty || host == string.Empty
                    || (port == 80 && protocol == "https")
                    || (port == 443 && protocol == "http"))
                {
                    Console.WriteLine("Invalid URL");
                }
                else
                {
                    Console.WriteLine($"Protocol: {protocol}");
                    Console.WriteLine($"Host: {host}");
                    Console.WriteLine($"Port: {port}");
                    Console.WriteLine($"Path: {path}");

                    if (uri.Query != string.Empty)
                    {
                        Console.WriteLine($"Query: {uri.Query}");
                    }

                    if (uri.Fragment != string.Empty)
                    {
                        Console.WriteLine($"Fragment: {uri.Fragment.Substring(1)}");
                    }
                }
            }
            catch (UriFormatException uriEx)
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}
