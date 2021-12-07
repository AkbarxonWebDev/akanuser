using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data.Address;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiForStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _db;

        public AddressController(IAddress db)
        {
            _db = db;
        }
        // GET: api/<AddressController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetAll());
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var address = await _db.Get(id);
            if (address==null)
            {
                return NotFound(address);
            }
            return Ok(address);
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Address address)
        {
            if (address==null)
            {
                return NotFound(address);
            }
            if (!await _db.Post(address))
            {
                return BadRequest();
            }
            return CreatedAtAction("Get",new { id = address.Id },address);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]Address address)
        {
            var response = await _db.Get(id);
            if (response==null)
            {
                return NotFound(address);
            }
            if (address==null)
            {
                return BadRequest(address);
            }
            if (!await _db.Put(id, address))
            {
                return BadRequest();
            }
            return Ok(address);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _db.Get(id);
            if (response == null)
            {
                return BadRequest();
            }
            if (!await _db.Delete(id))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
