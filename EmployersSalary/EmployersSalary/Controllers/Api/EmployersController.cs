﻿using System.Linq;
using System.Web.Http;
using EmployersSalary.Models;
using EmployersSalary.Business;
using EmployersSalary.Services;

namespace EmployersSalary.Controllers.Api
{
    //[Authorize]
    public class EmployersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET /api/employers
        [Route("api/employers")]
        //[Authorize(Roles = RoleName.Admin + "," + RoleName.ProjectManager)]
        public IHttpActionResult GetEmployers()
        {
            var employers = _unitOfWork.Employers.GetEmployers().ToList();

            if (employers == null || !employers.Any())
                return NotFound();

            return Ok(employers);
        }

        // GET /api/employers?firstname=firstName&lastname=lastName
        [Authorize(Roles = RoleName.Admin + "," + RoleName.ProjectManager)]
        [Route("api/employer")]
        public IHttpActionResult GetEmployer([FromUri] string firstName, string lastName)
        {
            var employer = _unitOfWork.Employers.GetEmployer(firstName, lastName);

            if (employer == null || employer.IsDisabled == true)
                return NotFound();

            return Ok(employer);
        }

        // PUT /api/employers?firstname=firstName&lastname=lastName
        [Authorize(Roles = RoleName.Admin)]
        [HttpPut]
        [Route("api/employers")]
        public IHttpActionResult UpdateEmployerSalary([FromUri] string firstName, string lastName, Employer employer)
        {
            var employerInDb = _unitOfWork.Employers.GetEmployer(firstName, lastName);

            if (employerInDb == null)
                return NotFound();

            if (!employer.NetSalary.HasValue)
                return BadRequest();

            employerInDb.NetSalary = employer.NetSalary;

            _unitOfWork.Comlete();

            return Ok();
        }

        // DELETE /api/employers?firstname=firstName&lastname=lastName
        [Authorize(Roles = RoleName.Admin)]
        [HttpDelete]
        [Route("api/employers")]
        public IHttpActionResult DeleteEmployer([FromUri] string firstName, string lastName)
        {
            var employerInDb = _unitOfWork.Employers.GetEmployer(firstName, lastName);

            if (employerInDb == null)
                return NotFound();

            if (employerInDb.IsDisabled == true)
                return BadRequest();

            employerInDb.Disable();

            _unitOfWork.Comlete();

            return Ok();
        }
    }
}