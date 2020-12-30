using KeyPay.Data.Dto.Site.Admin.BankCards;
using KeyPay.Data.Dto.Site.Admin.Photos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Dto.Site.Admin.Users
{
    public class UserForListDto
    {
        public UserForListDto()
        {
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public ICollection<PhotoForUserDetailDto> Photoes { get; set; }


        public ICollection<BankCardForUserDetailDto> BankCards { get; set; }

        




    }
}
