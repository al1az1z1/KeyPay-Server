using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KeyPay.Common.Helpers;
using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Dto.Site.Admin.Photos;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KeyPay.Presentation.Controllers.Site.Admin
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "Site")]
    [Route("site/admin/{userId}/photos")]
    [ApiController]
    public class PhotosController : Controller
    {

        public PhotosController(IUnitOfWork<KeyPayDbContext> dbContext, AutoMapper.IMapper mapper
            ,IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _db = dbContext;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.APIKey,
                _cloudinaryConfig.Value.APISecret
                );

            _cloudinary = new Cloudinary(acc);

                       
        }

        private readonly IUnitOfWork<KeyPayDbContext> _db;

        private readonly AutoMapper.IMapper _mapper;

        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        private readonly Cloudinary _cloudinary;


        [HttpPost]

        public async Task<IActionResult> ChangeUserPhoto(Guid id, PhotoForProfileDto photoForProfileDto)
        {
            if (id.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized("شما اجازه تغییر تصویر را ندارید");
            }

            var userFromDb = await _db.UserRepository.GetByIdAsync(id);

            var file = photoForProfileDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length> 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(250).Height(250).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                    
                    

                }
            }

            photoForProfileDto.Url = uploadResult.Uri.ToString();

            photoForProfileDto.PublicId = uploadResult.PublicId;


            var photo = _mapper.Map<Photo>(photoForProfileDto);


            await _db.PhotoRepository.InsertAsync(photo);

            if (await _db.SaveAsync())
            {
                return Ok();
            }
            else
            {
                return BadRequest("دوبار تلاش کنید");
            }


        }
    }


    



}
