using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet]

        public async Task<IActionResult> Register(string username, string password)
        {

            username = username.ToLower();
            if (!await _db.UserRepository.UserExist(username))
            {
                return BadRequest("There is an username as like this");
            }
            var user = new User()
            {
                Address = "",
                City = "",
                DateOfBirth = System.DateTime.Now,
                Gender = "",
                IsActive = true,
                Name = "",
                PhoneNumber = "",
                Status = true,
                UserName = "",
            };

           var createdUser =  await _authService.Register(user, password);

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
