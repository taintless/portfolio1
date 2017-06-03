using System.Linq;
using System.Web.Http;
using EmployersSalary.Models;
using EmployersSalary.Business;

namespace EmployersSalary.Controllers.Api
{
    [Authorize]
    public class EmployersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployersBusiness _employersBusiness;

        public EmployersController()
        {
            _context = new ApplicationDbContext();
            _employersBusiness = new EmployersBusiness(_context);
        }

        // GET /api/employers
        [Route("api/employers")]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.ProjectManager)]
        public IHttpActionResult GetEmployers()
        {
            var employers = _employersBusiness.GetEmployers().ToList();

            return Ok(employers);
        }

        // GET /api/employers?firstname=firstName&lastname=lastName
        [Authorize(Roles = RoleName.Admin + "," + RoleName.ProjectManager)]
        [Route("api/employer")]
        public IHttpActionResult GetEmployer([FromUri] string firstName, string lastName)
        {
            var employer = _employersBusiness.GetEmployer(firstName, lastName);

            if (employer == null)
                return NotFound();

            return Ok(employer);
        }

        // PUT /api/employers?firstname=firstName&lastname=lastName
        [Authorize(Roles = RoleName.Admin)]
        [HttpPut]
        [Route("api/employers")]
        public IHttpActionResult UpdateEmployerSalary([FromUri] string firstName, string lastName, Employer employer)
        {
            var employerInDb = _employersBusiness.GetEmployer(firstName, lastName);

            if (employerInDb == null)
                return NotFound();

            if (!employer.NetSalary.HasValue)
                return BadRequest();

            employerInDb.UpdateSalary(employer.NetSalary ?? default(float));

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/employers?firstname=firstName&lastname=lastName
        [Authorize(Roles = RoleName.Admin)]
        [HttpDelete]
        [Route("api/employers")]
        public IHttpActionResult DeleteEmployer([FromUri] string firstName, string lastName)
        {
            var employerInDb = _employersBusiness.GetEmployer(firstName, lastName);

            if (employerInDb == null)
                return NotFound();

            employerInDb.Disable();

            _context.SaveChanges();

            return Ok();
        }
    }
}