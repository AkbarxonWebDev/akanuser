using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data.Teacher_App;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiForStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _db;

        public TeacherController(ITeacherService db)
        {
            _db = db;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetAll());
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _db.Get(id);
            if (res==null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(teacher);
            }
            if (teacher == null)
            {
                return NotFound(teacher);
            }
            if (!await _db.Post(teacher))
            {
                return BadRequest();
            }
            return CreatedAtAction("Get",new { id = teacher.Id },teacher);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Teacher teacher)
        {
            var res = await _db.Get(id);
            if (res==null)
            {
                return NotFound(teacher);
            }
            if (teacher == null)
            {
                return BadRequest();
            }
            
            if(!await _db.Put(id,teacher))
            {
                return BadRequest();
            }
            return Ok(teacher);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _db.Get(id);
            if (res == null)
            {
                return NotFound();
            }
            if (!await _db.Delete(id))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
