namespace _01.URLDecode
{
    using System;
    using System.Net;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var inputUrl = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(inputUrl);

            Console.WriteLine(decodedUrl);
        }
    }
}
