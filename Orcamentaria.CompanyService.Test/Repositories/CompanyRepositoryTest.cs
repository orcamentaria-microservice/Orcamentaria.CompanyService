using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Test.Contexts;
using Orcamentaria.CompanyService.Test.Fixtures;
using Orcamentaria.Lib.Test.Repositories;
using Xunit;

namespace Orcamentaria.CompanyService.Test.Repositories
{
    [Collection(nameof(CompanyCollection))]
    public class CompanyRepositoryTest
    {
        public CompanyRepositoryTest()
        {
        }
    }

    [Collection(nameof(CompanyCollection))]
    public class CompanyReadRepositoryTest : ReadWithoutCompanyRepositoryTests<Company, MySqlContextTest>
    {
        public CompanyReadRepositoryTest(CompanyFixture fixture) : base(fixture) { }
    }

    [Collection(nameof(CompanyCollection))]
    public class CompanyWriteRepositoryTest : WriteWithCompanyRepositoryTests<Company, MySqlContextTest>
    {
        public CompanyWriteRepositoryTest(CompanyFixture fixture) : base(fixture) { }
    }
}
