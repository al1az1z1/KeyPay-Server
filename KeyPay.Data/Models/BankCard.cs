using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Models
{
    public class BankCard : BaseEntity<System.Guid>
    {
        public BankCard()
        {
            Id = System.Guid.NewGuid();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        #region Foriegn Fields

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid UserId { get; set; }

        public User User { get; set; }

        #endregion /Foriegn Fields


        [System.ComponentModel.DataAnnotations.StringLength(50,MinimumLength =0)]

        [System.ComponentModel.DataAnnotations.Required]
        public string BankName { get; set; }



        [System.ComponentModel.DataAnnotations.StringLength(50, MinimumLength = 0)]

        [System.ComponentModel.DataAnnotations.Required]
        public string OwnerName { get; set; }


        [System.ComponentModel.DataAnnotations.StringLength(50, MinimumLength = 0)]

        [System.ComponentModel.DataAnnotations.Required]
        public string Shaba { get; set; }



        [System.ComponentModel.DataAnnotations.StringLength(16, MinimumLength = 0)]
        [System.ComponentModel.DataAnnotations.Required]
        public string CardNumber { get; set; }


        [System.ComponentModel.DataAnnotations.Required]

        [System.ComponentModel.DataAnnotations.StringLength(maximumLength:2, MinimumLength =2)]
        public string ExpireDateMonth { get; set; }

        
        [System.ComponentModel.DataAnnotations.Required]

        [System.ComponentModel.DataAnnotations.StringLength(maximumLength: 2, MinimumLength = 2)]
        public string ExpireDateYear { get; set; }

       


    }
}
