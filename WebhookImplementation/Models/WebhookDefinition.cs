using System;
using System.Collections.Generic;
using System.Text;

namespace WebhookImplementation.Models
{
    public class WebhookDefinition
    {
        public string Uri { get; set; }
        public string MethodAuthorization { get; set; }
        public string Token { get; set; }
        public string MethodTry { get; set; }

        public dynamic Data { get; set; }
    } 
} 
 