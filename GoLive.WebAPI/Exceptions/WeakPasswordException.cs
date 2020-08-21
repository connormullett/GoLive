using System;

namespace GoLive.Exceptions
{
    internal class WeakPasswordException : Exception
    {
        public WeakPasswordException()
        {
        }

        public WeakPasswordException(string message) : base(message)
        {
        }

        public WeakPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}