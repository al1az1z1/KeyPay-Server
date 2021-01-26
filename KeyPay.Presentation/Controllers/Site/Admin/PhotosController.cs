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
    [Route("site/admin/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : Controller
    {


        #region Constructor
        public PhotosController(IUnitOfWork<KeyPayDbContext> dbContext, AutoMapper.IMapper mapper
            , IOptions<CloudinarySettings> cloudinaryConfig)
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

        #endregion /Constructor


        #region PrivateFields
        private readonly IUnitOfWork<KeyPayDbContext> _db;

        private readonly AutoMapper.IMapper _mapper;

        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        private readonly Cloudinary _cloudinary;

        #endregion /PrivateFields

        #region GetPhoto

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(System.Guid id)
        {
            var photoFromRepo = await _db.PhotoRepository.GetByIdAsync(id);
            var photo = _mapper.Map<PhotoForReturnProfileDto>(photoFromRepo);

            return Ok(photo);
        }

        #endregion /GetPhoto


        #region ChangeUserPhoto

        [HttpPost]

        public async Task<IActionResult> ChangeUserPhoto(Guid userId, [FromForm] PhotoForProfileDto photoForProfileDto)
        {
            if (userId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized("شما اجازه تغییر تصویر را ندارید");
            }

            var userFromDb = await _db.UserRepository.GetByIdAsync(userId);

            var file = photoForProfileDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
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

            photoForProfileDto.Url = uploadResult.Url.ToString();

            photoForProfileDto.PublicId = uploadResult.PublicId;


            var oldPhoto = await _db.PhotoRepository.GetAsync(p => p.UserId == userId && p.IsMain);


            // اگر کاربر عکسی در کلودینری   داشت پاکش کن  که حجم الکی نگیره 
            if (oldPhoto.PublicId != null && oldPhoto.PublicId !="0")
            {
                var deleteParam = new DeletionParams(oldPhoto.PublicId);
                var deleteResult = _cloudinary.Destroy(deleteParam);
            }
            
            _mapper.Map(photoForProfileDto, oldPhoto);
            
            _db.PhotoRepository.Update(oldPhoto);

            //await _db.PhotoRepository.InsertAsync(photo);

            if (await _db.SaveAsync())
            {
                var photoForReturn = _mapper.Map<PhotoForReturnProfileDto>(oldPhoto);
                return CreatedAtRoute("GetPhoto", routeValues: new { id = oldPhoto.Id }, photoForReturn);

            }
            else
            {
                return BadRequest("دوبار تلاش کنید");
            }


        }

        #endregion /ChangeUserPhoto





    }






}
