using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Dto.Site.Admin.BankCards
{
    public class BankCardForUserDetailDto
    {

        public string BankName { get; set; }

        public string OwnerName { get; set; }

        public string Shaba { get; set; }

        public string CardNumber { get; set; }

        public string ExpireDateMonth { get; set; }

        public string ExpireDateYear { get; set; }
    }
}
