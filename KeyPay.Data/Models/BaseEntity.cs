using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Models
{
    public abstract class BaseEntity <T>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public T Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime Created { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime Modified { get; set; }
    }
}
