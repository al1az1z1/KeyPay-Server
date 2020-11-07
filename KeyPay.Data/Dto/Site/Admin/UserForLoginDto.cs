using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyPay.Data.Dto.Site.Admin
{
    public class UserForLoginDto
    {

        [Required]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمیباشد")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsRemember { get; set; }


    }
}
