using System;

namespace GoLive.Exceptions
{
    internal class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException()
        {
        }

        public ProjectNotFoundException(string message) : base(message)
        {
        }

        public ProjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}