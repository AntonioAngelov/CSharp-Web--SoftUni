﻿namespace WebServer.Server.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        private const string InvalidRequestMessage = "Request is not valid.";

        public static object FromInvalidRequest()
            => throw new BadRequestException(InvalidRequestMessage);

        public BadRequestException(string message)
            : base(message)
        {

        }
    }
}
