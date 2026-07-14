using Microsoft.EntityFrameworkCore;
using Orcamentaria.CompanyService.Infrastructure.Contexts;

namespace Orcamentaria.CompanyService.Test.Contexts
{
    public class MySqlContextTest : MySqlContext
    {
        public MySqlContextTest(DbContextOptions<DbContext> options) : base(options) { }
    }
}
