using System;
using System.Collections.Generic;
using System.Text;

namespace WebhookImplementation.Exceptions
{
    internal class ClientApiException : Exception
    {
        public int ErrorCode { get; }

        public ClientApiException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
