using KeyPay.Data.Dto.Site.Admin.BankCards;
using KeyPay.Data.Dto.Site.Admin.Photos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Dto.Site.Admin.Users
{
    public class UserForDetailDto
    {
        public UserForDetailDto()
        {
        }
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime LastActive { get; set; }

        public string City { get; set; }

        public bool IsActive { get; set; }

        public bool Status { get; set; }

        public string PhotoUrl { get; set; }

        public int Age { get; set; }




    }
}
