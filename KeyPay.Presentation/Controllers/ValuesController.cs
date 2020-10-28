using System;
using System.Collections.Generic;
using System.Linq;
using KeyPay.Repo.Infrastructure;
using KeyPay.Data.Models;
using Microsoft.AspNetCore.Mvc;
using KeyPay.Services.Auth.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyPay.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {



        public ValuesController(IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> dbcontext, IAuthService authService)
        {
            _db = dbcontext;
            _authService = authService;

        }

        private readonly IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> _db;

        private readonly IAuthService _authService;

        // GET: api/<ValuesController>
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<string>>> GetAsync()
        {

            

            //test Unit of work and repos
            var user = new User()
            {
                Address = "",
                City = "",
                DateOfBirth = System.DateTime.Now,
                Gender = "",
                IsActive = true,
                Name = "",
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },

                PhoneNumber = "",
                Status = true,
                UserName = "",


            };


            var u = await _authService.Register(user, "password");


            return Ok(u);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<ActionResult<string>> GetAsync(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async System.Threading.Tasks.Task<string> PostAsync([FromBody] string value)
        {
            return null;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<string> PutAsync(int id, [FromBody] string value)
        {
            return null;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<string> DeleteAsync(int id)
        {
            return null;
        }
    }
}
