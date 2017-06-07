using EmployersSalary.Models;

namespace EmployersSalary.Services
{
    public interface IApplicationUserBusiness
    {
        ApplicationUser GetUser(string id);
    }
}