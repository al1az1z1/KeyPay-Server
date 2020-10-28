using System;
using System.Collections.Generic;
using System.Linq;
using KeyPay.Data.Infrastructure;
using KeyPay.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyPay.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> _db;

        public ValuesController(IUnitOfWork<Data.DatabaseContext.KeyPayDbContext> dbcontext)
        {
            _db = dbcontext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<string>>> GetAsync()
        {

            return Ok("it's Ok");

            //test Unit of work and repos
            //var user = new User()
            //{
            //    Address = "",
            //    City = "",
            //    DateOfBirth = System.DateTime.Now,
            //    Gender = "",
            //    IsActive = true,
            //    Name = "",
            //    PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },
            //    PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },

            //    PhoneNumber = "",
            //    Status = true,
            //    UserName = "",


            //};

            //await _db.IUserRepository.InsertAsync(user);
            //await _db.SaveAsync();

            //var model = await _db.IUserRepository.GetAllAsync();

            //return Ok(model);
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
