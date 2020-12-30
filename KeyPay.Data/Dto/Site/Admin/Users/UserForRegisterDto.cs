using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyPay.Data.Dto.Site.Admin.Users
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نمیباشد")]
        public string UserName { get; set; }

        [StringLength(10, MinimumLength =5 , ErrorMessage ="پسورد باید بین 5 تا 10 کاراکتر باشد" )]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage ="{0} الزامیست")]
        
        public string Name { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string PhoneNumber { get; set; }

    }
}
