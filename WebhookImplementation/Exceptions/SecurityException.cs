using System;
using System.Collections.Generic;
using System.Text;

namespace WebhookImplementation.Exceptions
{
    internal class SecurityException : Exception
    {
        public int ErrorCode { get; }

        public SecurityException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
