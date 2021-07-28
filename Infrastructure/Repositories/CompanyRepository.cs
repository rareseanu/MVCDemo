using System.Linq;
using Application.RepositoryInterfaces;
using Domain;
using Domain.Companies;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        protected override IQueryable<Company> DefaultIncludes(IQueryable<Company> queryable)
        {
            return  queryable.Include(p => p.Employees);
        }
    }
}
