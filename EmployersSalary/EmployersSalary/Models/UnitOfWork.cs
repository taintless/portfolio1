using EmployersSalary.Business;
using EmployersSalary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployersSalary.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmployersBusiness Employers { get; set; }
        public IApplicationUserBusiness Users { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employers = new EmployersBusiness(context);
            Users = new ApplicationUserBusiness(context);
        }
        public void Comlete()
        {
            _context.SaveChanges();
        }
    }
}