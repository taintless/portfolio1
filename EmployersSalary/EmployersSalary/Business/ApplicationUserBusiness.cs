using EmployersSalary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployersSalary.Business
{
    public class ApplicationUserBusiness
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.Include(u => u.Employer).Single(u => u.Id == id);
        }
    }
}