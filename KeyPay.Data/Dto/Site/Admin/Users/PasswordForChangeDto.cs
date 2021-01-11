using System.ComponentModel.DataAnnotations;

namespace KeyPay.Data.Dto.Site.Admin.Users
{
   public class PasswordForChangeDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "پسورد باید بین 5 تا 10 کاراکتر باشد")]
        public string NewPassword { get; set; }
    }
}
