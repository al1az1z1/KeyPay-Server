using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KeyPay.Common.ErrorMessages;
using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Dto.Site.Admin;
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
    [Authorize]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> dbcontext, IAuthService authService, IConfiguration configuration)
        {
            _db = dbcontext;
            _authService = authService;
            _config = configuration;

        }

        private readonly IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> _db;

        private readonly IAuthService _authService;

        private readonly IConfiguration _config;

        /// <summary>
        /// Get controller
        /// </summary>
        /// <returns></returns>
        #region Register
        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<IActionResult> Register(Data.Dto.Site.Admin.UserForRegisterDto userForRegisterDto)
        {

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExist(userForRegisterDto.UserName))
            {
                return BadRequest(new KeyPay.Common.ErrorMessages.Messages()
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

                UserName = userForRegisterDto.UserName,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                DateOfBirth = System.DateTime.Now,
                Gender = true,
                IsActive = true,
                Status = true,
                Address = "",

            };

            var createdUser = await _authService.Register(user, userForRegisterDto.Password);

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

                return Unauthorized(new Messages()
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

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(500, ex.Message);
            //}



        }

        #endregion /Login

        #region test authorize

        [AllowAnonymous]
        [HttpGet("getvalue")]
        public async Task<IActionResult> GetValueAsync()
        {

            return Ok(new Messages()
            {
                status = true,
                message = "ok",
            });
        }


        [HttpPost("getvalue")]
        public async Task<IActionResult> PostValueAsync()
        {

            return Ok(new Messages()
            {
                status = true,
                message = "ok",
            });
        }
        #endregion /test authorize




    }
}
