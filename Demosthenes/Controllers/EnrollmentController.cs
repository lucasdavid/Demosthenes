using Demosthenes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Demosthenes.Services.Exceptions;
using Demosthenes.Core;
using System.Security.Authentication;

namespace Demosthenes.Controllers
{
    [RoutePrefix("api/Enrollment")]
    public class EnrollmentController : ApiController
    {
        private EnrollmentService _enrollments;

        public EnrollmentController(EnrollmentService enrollments)
        {
            _enrollments = enrollments;
        }

        // GET: api/Enrollment/Student/5
        [Route("Student/{studentId}")]
        public async Task<ICollection<Enrollment>> GetStudentEnrollments(string studentId)
        {
            return await _enrollments.OfStudent(studentId);
        }

        // GET: api/Enrollment/Class/5
        [Route("Student/{classId}")]
        public async Task<ICollection<Enrollment>> GetStudentEnrollments(int classId)
        {
            return await _enrollments.OfClass(classId);
        }

        // POST: api/Enrollment/Enroll/Student/5/Class/5
        [Authorize]
        [Route("Enroll/Student/{studentId}/Class/{classId}")]
        public async Task<IHttpActionResult> postEnroll(string studentId, int classId)
        {
            try
            {
                if (User.Identity.GetUserId() != studentId && !User.IsInRole("admin"))
                {
                    throw new AuthenticationException();
                }

                await _enrollments.Enroll(studentId, classId);
                return Ok();
            }
            catch(AuthenticationException)
            {
                ModelState.AddModelError("Authorization", "You don't have authority to enroll this student.");
            }
            catch (StudentAlreadyEnrolledException)
            {
                ModelState.AddModelError("StudentAlreadyEnrolledException"
                    , User.Identity.GetUserId() == studentId
                    ? "You're already enrolled in this class."
                    : "Student's already enrolled in this class.");
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Enrollment/Unenroll/5
        [Authorize]
        [Route("Unenroll/{enrollmentId}")]
        public async Task<IHttpActionResult> DeleteUnenroll(int enrollmentId)
        {
            try
            {
                var e = await _enrollments.Find(enrollmentId);

                if (e == null)
                {
                    throw new NullReferenceException();
                }

                if (User.Identity.GetUserId() != e.StudentId && !User.IsInRole("admin"))
                {
                    throw new AuthenticationException();
                }

                await _enrollments.Unenroll(enrollmentId);
                return Ok();
            }
            catch (AuthenticationException)
            {
                ModelState.AddModelError("AuthenticationException", "You don't have authority to enroll this student.");
            }
            catch (NullReferenceException)
            {
                ModelState.AddModelError("KeyNotFoundException", "Enrollment not found.");
            }
            catch (KeyNotFoundException)
            {
                ModelState.AddModelError("KeyNotFoundException", "Enrollment not found.");
            }
            catch (StudentNotEnrolledException)
            {
                ModelState.AddModelError("StudentNotEnrolledException", "Student is not yet enrolled in class.");
            }

            return BadRequest(ModelState);
        }
    }
}
