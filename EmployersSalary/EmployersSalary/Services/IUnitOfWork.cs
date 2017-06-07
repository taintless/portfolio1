namespace EmployersSalary.Services
{
    public interface IUnitOfWork
    {
        IApplicationUserBusiness Users { get; set; }
        IEmployersBusiness Employers { get; set; }
        void Comlete();
    }
}