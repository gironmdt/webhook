using System;
using System.Collections.Generic;
using System.Text;

namespace WebhookImplementation.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse()
        {
            Success = false;
            Data = default(T);
            ErrorMessage = string.Empty;
        }

        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
            ErrorMessage = string.Empty;
        }

        public ApiResponse(string errorMessage)
        {
            Success = false;
            Data = default(T);
            ErrorMessage = errorMessage;
        }
    }

}
