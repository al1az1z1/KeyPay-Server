using System.Collections.Generic;
using System.Threading.Tasks;
using KeyPay.Common.ErrorMessages;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using KeyPay.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KeyPay.Presentation.Controllers.Site.Admin
{
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> dbcontext, IAuthService authService)
        {
            _db = dbcontext;
            _authService = authService;

        }

        private readonly IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> _db;

        private readonly IAuthService _authService;

        /// <summary>
        /// Get controller
        /// </summary>
        /// <returns></returns>

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
                Address="",

            };

            var createdUser = await _authService.Register(user, userForRegisterDto.Password);

            return StatusCode(201);




        }

        //public async System.Threading.Tasks.Task<ActionResult<IEnumerable<string>>> GetAsync()
        //{



        //    //test Unit of work and repos
        //    var user = new User()
        //    {
        //        Address = "",
        //        City = "",
        //        DateOfBirth = System.DateTime.Now,
        //        Gender = "",
        //        IsActive = true,
        //        Name = "",
        //        PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },
        //        PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },

        //        PhoneNumber = "",
        //        Status = true,
        //        UserName = "",


        //    };


        //    var u = await _authService.Register(user, "password");


        //    return Ok(u);
        //}
    }
}
