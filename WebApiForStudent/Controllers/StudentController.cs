using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data.Student;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiForStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _db;

        public StudentController(IStudentService db)
        {
            _db = db;
        }

        // GET: api/<StudentController>
        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            return Ok(await _db.GetAll());
        }

        #endregion


        // GET api/<StudentController>/5
        #region GetId
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(Guid id)
        {
            return Ok(await _db.Get(id));
        }
        #endregion


        // POST api/<StudentController>
        #region Post
        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(student);
            }
            if (student == null)
            {
                return NotFound(student);
            }

            if (!await _db.Post(student))
            {
                return BadRequest();
            }
            return CreatedAtAction("Get", new { id = student.Id }, student);


            
        }
        #endregion


        // PUT api/<StudentController>/5
        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(Guid id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(student);
            }
            if (await _db.Get(id)==null)
            {
                return NotFound();
            }

            if (!await _db.Put(id, student))
            {
                return BadRequest("Data Error");
            }
            return NoContent();

        }
        #endregion


        // DELETE api/<StudentController>/5
        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(Guid id)
        {
            var student = await _db.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            if (!await _db.Delete(id))
            {
                return BadRequest();
            }
            return NoContent();

        }
        #endregion

       
    }
}
