using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Infrastructure.Contexts;
using Orcamentaria.Lib.Domain.Contexts;
using Orcamentaria.Lib.Infrastructure.Repositories;

namespace Orcamentaria.CompanyService.Infrastructure.Repositories
{
    public class CompanyRepository : BasicRepository<Company>, ICompanyRepository
    {
        private readonly MySqlContext _dbContext;

        public CompanyRepository(
            MySqlContext dbContext,
            IUserAuthContext userAuthContext)
            : base(dbContext, userAuthContext)
        {
            _dbContext = dbContext;
        }
    }
}
