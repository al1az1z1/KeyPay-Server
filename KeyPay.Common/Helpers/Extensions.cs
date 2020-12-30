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


        public static int ToAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;


            // برای اینکه بعد از تفریق یک سال سن رو بالاتر میگیره 
            if (dateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;


        }
    }
}
