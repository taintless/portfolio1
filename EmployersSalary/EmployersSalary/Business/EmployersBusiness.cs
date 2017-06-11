using EmployersSalary.Models;
using EmployersSalary.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EmployersSalary.Business
{
    public class EmployersBusiness : IEmployersBusiness
    {
        private readonly IApplicationDbContext _context;

        public EmployersBusiness(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employer> GetEmployers()
        {
            return _context.Employers.Where(e => e.FirstName != "Admin" && !e.IsDisabled);
        }

        public Employer GetEmployer(string firstName, string lastName)
        {
            return _context.Employers.SingleOrDefault(c => c.FirstName == firstName && c.LastName == lastName);            
        }

    }
}