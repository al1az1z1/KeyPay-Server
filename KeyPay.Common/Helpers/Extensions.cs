using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Common.Helpers
{
   public static class Extensions
    {
        
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("Add-Error", message);
            response.Headers.Add("Access-Control-Expose", "App-error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
