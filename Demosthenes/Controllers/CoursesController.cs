using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Demosthenes.Core;
using Demosthenes.Services;
using Demosthenes.ViewModels;
using System.Linq;

namespace Demosthenes.Controllers
{
    public class CoursesController : ApiController
    {
        private CourseService _courses;

        public CoursesController(CourseService courses)
        {
            _courses = courses;
        }

        // GET: api/Courses
        public async Task<ICollection<CourseResultViewModel>> GetCourses()
        {
            var result = (await _courses.All())
                .Select(c => (CourseResultViewModel)c)
                .ToList();

            return result;
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            var course = (CourseResultViewModel) await _courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // GET: api/Courses/Departments/5
        [Route("api/Courses/Departments/{id}")]
        public async Task<ICollection<CourseResultViewModel>> GetCoursesOfDepartment(int id)
        {
            return (await _courses.OfDepartment(id))
                .Select(e => (CourseResultViewModel)e)
                .ToList();
        }

        // PUT: api/Courses/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCourse(CourseUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _courses.Find(model.Id);
            course.Title        = model.Title;
            course.Credits      = model.Credits;
            course.DepartmentId = model.DepartmentId;
            
            try
            {
                await _courses.Update(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_courses.Exists(course.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Courses
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> PostCourse(CourseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _courses.Add(new Course
            {
                Title        = model.Title,
                Credits      = model.Credits,
                DepartmentId = model.DepartmentId
            });

            return CreatedAtRoute("DefaultApi", new { id = course.Id }, (CourseResultViewModel)course);
        }

        // DELETE: api/Courses/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> DeleteCourse(int id)
        {
            var course = await _courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            await _courses.Delete(course);
            
            return Ok((CourseResultViewModel) course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _courses.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}