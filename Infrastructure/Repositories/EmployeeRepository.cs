using Application.RepositoryInterfaces;
using Domain.Employees;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
