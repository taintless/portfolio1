using EmployersSalary.Models;
using EmployersSalary.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EmployersSalary.Business
{
    public class EmployersBusiness : IEmployersBusiness
    {
        private readonly ApplicationDbContext _context;

        public EmployersBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employer> GetEmployers()
        {
            return _context.Employers.Where(e => e.RegularEmployer == true && !e.IsDisabled);
        }

        public Employer GetEmployer(int id)
        {
            return _context.Employers.SingleOrDefault(x => x.Id == id);            
        }

    }
}