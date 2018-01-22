namespace WebServer.Server.Exceptions
{
    using System;
    public class InvalideResponseException : Exception
    {
        public InvalideResponseException(string message)
            :base(message)
        {
            
        }
    }
}
