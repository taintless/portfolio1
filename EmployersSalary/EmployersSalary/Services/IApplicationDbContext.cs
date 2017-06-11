using EmployersSalary.Models;
using System.Data.Entity;

namespace EmployersSalary.Services
{
    public interface IApplicationDbContext
    {
        DbSet<Employer> Employers { get; set; }
    }
}