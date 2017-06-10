using EmployersSalary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployersSalary.Services
{
    public interface IEmployersBusiness
    {
        IEnumerable<Employer> GetEmployers();
        Employer GetEmployer(string firstName, string lastName);
    }
}