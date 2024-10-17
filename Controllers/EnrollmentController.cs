using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Database;
using WebApplication7.Database.Entities;
using WebApplication7.ViewsModel;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments(int takeNumber = 10)
        {
            return await _context.Enrollments.Include(enrollment => enrollment.Student)
                .Include(enrollment => enrollment.Student).Take(takeNumber).ToListAsync();
        }

        // GET: api/Enrollment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            return enrollment == null ? NotFound() : Ok(enrollment);
        }

        // GET: api/Enrollment/5/5
        [HttpGet("{studentId}/{courseId}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            return enrollment == null ? NotFound() : Ok(enrollment);
        }

        // PUT: api/Enrollment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return BadRequest();
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Enrollment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(EnrollmentAPIModel enrollment)
        {
            // // check existing student and course
            var studentInfo = await _context.Students.FirstOrDefaultAsync(s => s.Id == enrollment.StudentId);
            if (studentInfo == null)
            {
                return BadRequest("Student not found");
            }

            var courseInfo = await _context.Courses.FirstOrDefaultAsync(c => c.Id == enrollment.CourseId);
            if (courseInfo == null)
            {
                return BadRequest("Course not found");
            }
            // bool isStudentExist = await _context.Students.AnyAsync(s => s.Id == enrollment.StudentId);
            // if (!isStudentExist)
            // {
            //     return BadRequest("Student not found");
            // }
            //
            // bool isCourseExist = await _context.Courses.AnyAsync(c => c.Id == enrollment.CourseId);
            // if (!isCourseExist)
            // {
            //     return BadRequest("Course not found");
            // }

            // check if student already enrolled in the course
            bool iEnrolled = await _context.Enrollments.AnyAsync(e =>
                e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);
            if (iEnrolled)
            {
                return BadRequest("Student already enrolled in the course");
            }

            // create new enrollment
            var newEnroll = new Enrollment()
            {
                Student = studentInfo,
                Course = courseInfo,
                EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.Enrollments.Add(newEnroll);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetEnrollment", new { id = newEnroll.Id }, newEnroll);
            return Created(string.Empty, newEnroll);
        }

        // DELETE: api/Enrollment/5
        /// <summary>
        /// Deletes an enrollment by ID.
        /// </summary>
        /// <param name="id">The ID of the enrollment to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        /// <response code="204">No content if the deletion is successful.</response>
        /// <response code="404">Enrollment not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}