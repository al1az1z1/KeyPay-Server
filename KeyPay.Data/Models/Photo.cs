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



        [System.ComponentModel.DataAnnotations.Required]
        public string Url { get; set; }

        public string Alt { get; set; }
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string IsMain { get; set; }




    }
}
