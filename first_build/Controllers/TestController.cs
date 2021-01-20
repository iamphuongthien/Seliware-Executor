using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using first_build.Data;
using first_build.Models;

namespace first_build.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SchoolContext _context;

        public TestController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var tmp = await _context.Students.ToListAsync();
            return tmp;
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int? id)
        {
            var Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            return Student;
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student Student)
        {
            if (id != Student.ID)
            {
                return BadRequest();
            }

            _context.Entry(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Student")]
        public async Task<ActionResult<Student>> PostStudent(Student Student)
        {
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetStudent", new { id = Student.Id }, Student);
            return CreatedAtAction(nameof(GetStudent), new { id = Student.ID }, Student);
        }


        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Students")]
        public async Task<ActionResult<List<Student>>> PostStudent(List<Student> LStudent)
        {
            Student Student = new Student();
            for(int i = 0; i < LStudent.Count(); i++){

                Student = LStudent[i];
                _context.Students.Add(Student);
                await _context.SaveChangesAsync();
            }

            // return CreatedAtAction("GetStudent", new { id = Student.Id }, Student);
            return CreatedAtAction(nameof(GetStudent), new { id = Student.ID }, Student);
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            var Student = await _context.Students.FindAsync(id);
            if (Student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(long id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
