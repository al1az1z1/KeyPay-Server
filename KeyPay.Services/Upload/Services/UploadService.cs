using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Dto.Services;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using KeyPay.Services.Upload.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Services.Upload.Services
{
    public class UploadService : IUploadService
    {
        public UploadService(IUnitOfWork<KeyPayDbContext> dbContext)
        {
            _db = dbContext;

            _setting = _db.SettingRepository.GetById(1);

            Account acc = new Account(
                _setting.CloudinaryAPIKey,
                _setting.CloudinaryAPISecret,
                _setting.CloudinaryCloudName
                );

            _cloudinary = new Cloudinary(acc);
        }



        private readonly IUnitOfWork<KeyPayDbContext> _db;

        private readonly Cloudinary _cloudinary;

        private readonly Setting _setting;



        public Task<FileUploadDto> UploadFile(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public FileUploadDto UploadToCloudinary(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                try
                {

                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation().Width(250).Height(250).Crop("fill").Gravity("face")
                        };

                           uploadResult = _cloudinary.Upload(uploadParams);

                        if (string.IsNullOrEmpty(uploadResult.Error.Message))
                        {
                            return new FileUploadDto()
                            {
                                Status = true,
                                Message = "فایل با موفقیت در کلاو.دینری آپلود گردید",
                                PublicId = uploadResult.PublicId,
                                Url = uploadResult.Url.ToString()
                            };
                      

                        }
                        else
                        {
                            return new FileUploadDto()
                            {
                                Status = false,
                                Message = uploadResult.Error.Message
                            };
                        }

                    }

                    

                }
                catch (Exception ex)
                {

                    return new FileUploadDto()
                    {
                        Status = false,
                        Message = ex.Message
                    };
                }
            }

            else
            {
                return new FileUploadDto()
                {
                    Status = false,
                    Message = "عکسی یافت نشد "

                };


            }


        }

        public Task<FileUploadDto> UploadToLocal(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
