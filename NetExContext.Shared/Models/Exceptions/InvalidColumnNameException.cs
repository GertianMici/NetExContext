﻿namespace NetExContexts.Shared.Models.Exceptions
{
    public class InvalidColumnNameException : Exception
    {
        public InvalidColumnNameException(string? message) : base(message)
        {
        }
    }
}
