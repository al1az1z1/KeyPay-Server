using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyPay.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            return new string[] { "value1", "value2" };
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
        public async System.Threading.Tasks.Task<string>  DeleteAsync(int id)
        {
            return null;
        }
    }
}
