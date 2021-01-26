using KeyPay.Data.Dto.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Services.Upload.Interface
{
    public interface IUploadService
    {
        FileUploadDto UploadToCloudinary(IFormFile file);

        Task<FileUploadDto> UploadToLocal(IFormFile file);

        Task<FileUploadDto> UploadFile(IFormFile file);

    }
}
