using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KeyPay.Common.ErrorMessages;
using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Dto.Site.Admin.Users;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using KeyPay.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace KeyPay.Presentation.Controllers.Site.Admin
{

    [ApiExplorerSettings(GroupName = "Site")]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> dbcontext, IAuthService authService, IConfiguration configuration
            , AutoMapper.IMapper mapper)
        {
            _db = dbcontext;
            _authService = authService;
            _config = configuration;
            Mapper = mapper;

        }

        private readonly IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> _db;

        private readonly IAuthService _authService;

        private readonly IConfiguration _config;

        // روش جدید
        protected AutoMapper.IMapper Mapper { get; }

        /// <summary>
        /// Get controller
        /// </summary>
        /// <returns></returns>
        #region Register
        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<IActionResult> Register(Data.Dto.Site.Admin.Users.UserForRegisterDto userForRegisterDto)
        {

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExist(userForRegisterDto.UserName))
            {
                return BadRequest(new KeyPay.Common.ErrorMessages.ReturnMessages()
                {
                    code = 400,
                    status = false,
                    title = "خطا",
                    message = "نام کاربری یا رمز عبور موجود است",
                }
                );
            }
            //return BadRequest("There is an username as like this");
            var user = new User()
            {

                Name = userForRegisterDto.Name,
                UserName = userForRegisterDto.UserName,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = "2121",
                Gender = true,
                DateOfBirth = System.DateTime.Now,
                LastActive = System.DateTime.Now,
                IsActive = true,
                Status = true,

            };

            var photo = new Photo()
            {
                UserId = user.Id,
                //Url = "https://res.cloudinary.com/drtgpzrcl/image/upload/v1611409987/nophoto_sj6orv.png",
                Url = string.Format("{0}://{1}{2}{3}", Request.Scheme, Request.Host.Value, Request.PathBase.Value, "/File/Pic/profilepic.PNG"),
                Alt = "Profile pc main one",
                Description = "Profile pc main one",
                IsMain = true,
                PublicId = "0"
            };

            var createdUser = await _authService.Register(user, photo, userForRegisterDto.Password);

            return StatusCode(201);




        }

        #endregion /Register


        #region Login
        [AllowAnonymous]
        [HttpPost("login")]

        public async System.Threading.Tasks.Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
        {
            //try
            //{
            //throw exception and test extension class in Common and startup changes it is with header http
            //throw new Exception("auto error");

            //anither way for export error to front it is without http and like json result

            //return Unauthorized(new Messages()
            //{
            //    status = false,
            //    title = "خطا",
            //    message = "مرز عبور و نام کاربری اشتباه است",

            //});


            var userFromRepo = await _authService.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userFromRepo == null)

                return Unauthorized(new ReturnMessages()
                {
                    status = false,
                    title = "خطا",
                    message = "مرز عبور و نام کاربری اشتباه است",
                });


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name , userFromRepo.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // if IsRemember was true 2 days keep user login else keep user login 2 Hs
                Expires = userForLoginDto.IsRemember ? DateTime.Now.AddDays(2) : DateTime.Now.AddHours(2),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDes);

            var user = Mapper.Map<UserForDetailDto>(source: userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(500, ex.Message);
            //}



        }

        #endregion /Login

        //#region test authorize

        //[AllowAnonymous]
        //[HttpGet("getvalue")]
        //public async Task<IActionResult> GetValueAsync()
        //{

        //    return Ok(new Messages()
        //    {
        //        status = true,
        //        message = "ok",
        //    });
        //}


        //[HttpPost("getvalue")]
        //public async Task<IActionResult> PostValueAsync()
        //{

        //    return Ok(new Messages()
        //    {
        //        status = true,
        //        message = "ok",
        //    });
        //}
        //#endregion /test authorize




    }
}
