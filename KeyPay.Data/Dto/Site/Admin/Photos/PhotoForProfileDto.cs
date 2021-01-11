﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Dto.Site.Admin.Photos
{
    public class PhotoForProfileDto
    {
        
        public string Url { get; set; }

        public IFormFile File { get; set; }

        public string Alt { get; set; }

        public string Description { get; set; }

        public string PublicId { get; set; }

        public bool IsMain { get; set; }
    }
}
