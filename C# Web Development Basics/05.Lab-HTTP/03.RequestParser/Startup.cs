namespace _03.RequestParser
{
    using System;
    using System.Collections.Generic;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var pathsWithMethods = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var tokens = input
                    .Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                var path = $"/{tokens[0]}";

                if (pathsWithMethods.ContainsKey(path))
                {
                    pathsWithMethods[path].Add(tokens[1]);
                }
                else
                {
                    pathsWithMethods[path] = new HashSet<string>() {tokens[1]};
                }
            }

            var requestTokens = Console.ReadLine()
                .Split(' ');

            var requestMethod = requestTokens[0].ToLower();

            var resposeStatus = "HTTP/1.1 200 OK";
            var contentLength = "Content-Length: 2";
            var contentType = "Content-Type: text/plain";
            var statusText = "OK";

            if (!pathsWithMethods.ContainsKey(requestTokens[1])
                || !pathsWithMethods[requestTokens[1]].Contains(requestMethod))
            {
                resposeStatus = "HTTP/1.1 404 NotFound";
                contentLength = "Content-Length: 9";
                statusText = "NotFound";
            }

            Console.WriteLine(resposeStatus);
            Console.WriteLine(contentLength);
            Console.WriteLine(contentType);
            Console.WriteLine();
            Console.WriteLine(statusText);
        }
    }
}
