using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data.Course_App;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiForStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _db;

        public CourseController(ICourseService db)
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
            var course = await _db.Get(id);
            if (course==null)
            {
                return BadRequest();
            }
            return Ok(await _db.Get(id));
        }

        //error
        // POST api/<CourseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(course);
            }
            if (course==null)
            {
                return NotFound(course);
            }
           
             if (!await _db.Post(course))
            {
                return BadRequest("Data Error");
            }
            return CreatedAtAction("Get",new {id=course.Id},course);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]Course course)
        {
            var res = await _db.Get(id);
            if (res==null)
            {
                return NotFound(course);
            }
            if (course==null)
            {
                return BadRequest();
            }
            
            if (!await _db.Put(id,course))
            {
                return BadRequest();
            }
            return Ok(course);
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
