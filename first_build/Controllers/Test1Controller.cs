using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using first_build.Data;
using first_build.Models;
using first_build.Models.TbView;
using Newtonsoft.Json;

namespace first_build.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test1Controller : ControllerBase
    {
        private readonly SchoolContext _context;

        public Test1Controller(SchoolContext context){
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<Object>> GetEnrollments()
        {

            // var tmp1 = JsonConvert.SerializeObject(tmp2);
            return await GetEnrollment().ToListAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetEnrollment(int? id)
        {
            var Enrollment = await GetEnrollment().Where(e => e.ID_enrollment == id).ToListAsync();

            if (Enrollment == null)
            {
                return NotFound();
            }

            return Enrollment;
        }

        public IQueryable<V_3Table> GetEnrollment(){

            // var Tmp = _context.Enrollments.Join(_context.Students, e => e.StudentID, s => s.ID,(e, s) => new {
            //     ID_e = e.EnrollmentID,
            //     ID_c = e.CourseID,
            //     Name = s.FirstMidName,
            //     Date_s = s.EnrollmentDate
            // }).AsQueryable().Join(_context.Courses, e => e.ID_c, c => c.CourseID, (e,c) => new {
            //     ID_enrollment = e.ID_e,
            //     NameStudent = e.Name,
            //     Title_c = c.Title
            // }).AsQueryable();

            return _context.View3Table.AsQueryable();
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(long id, V_3Table Student)
        {
            if (id != Student.ID_enrollment)
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

        private bool StudentExists(long id)
        {
            return _context.View3Table.Any(e => e.ID_enrollment == id);
        }
    }

}