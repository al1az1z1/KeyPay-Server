using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Models
{
    public class Photo : BaseEntity<int>
    {
        public Photo()
        {
            Id = new int();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        #region Foriegn Fields

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid UserId { get; set; }

        public User User { get; set; }

        #endregion /Foriegn Fields



        [System.ComponentModel.DataAnnotations.StringLength(1000, MinimumLength = 0)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Url { get; set; }


        [System.ComponentModel.DataAnnotations.StringLength(500, MinimumLength = 0)]
        public string Alt { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(300, MinimumLength = 0)]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public bool IsMain { get; set; }




    }
}
