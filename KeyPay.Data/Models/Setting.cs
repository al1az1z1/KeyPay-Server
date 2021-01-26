using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Models
{
    public class Setting : BaseEntity<short>
    {

        public Setting()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        

        [System.ComponentModel.DataAnnotations.Required]
        public string CloudinaryCloudName { get; set; }



        [System.ComponentModel.DataAnnotations.Required]

        public string CloudinaryAPIKey { get; set; }


        [System.ComponentModel.DataAnnotations.Required]

        public string CloudinaryAPISecret { get; set; }




    }
}
