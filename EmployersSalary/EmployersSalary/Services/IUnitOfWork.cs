namespace EmployersSalary.Services
{
    public interface IUnitOfWork
    {
        IApplicationUserBusiness Users { get; }
        IEmployersBusiness Employers { get; }
        void Comlete();
    }
}