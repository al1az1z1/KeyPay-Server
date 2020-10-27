﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Models
{
    public class User : BaseEntity<System.Guid>
    {
        public User()
        {
            Id = System.Guid.NewGuid();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        #region Relational Fieldds
        /// <summary>
        /// عکس طرف
        /// </summary>
        public ICollection<Photo> Photoes { get; set; }

        public ICollection<BankCard> BankCards { get; set; }

        #endregion / Relational Fields



        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string PhoneNumber { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Address { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public byte[] PasswordHash { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public byte[] PasswordSalt { get; set; }



        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public bool IsActive { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public bool Status { get; set; }

      




    }
}
