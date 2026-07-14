using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.Lib.Test.Fixtures;
using Xunit;

namespace Orcamentaria.CompanyService.Test.Fixtures
{
    [CollectionDefinition(nameof(CompanyCollection))]
    public class CompanyCollection : ICollectionFixture<CompanyFixture> { }
    public class CompanyFixture : BaseFixture<Company>
    {
        override
        public Company CreateEntity(long id)
        {
            return new Company
            {
                Id = id,
                CorporateName = Faker.Name.FirstName(),
                TradeName = Faker.Name.LastName(),
                Cnpj = Faker.Random.ReplaceNumbers("#########"),
                Address = Faker.Address.StreetAddress(),
                TelePhone = Faker.Phone.PhoneNumber(),
                CellPhone = Faker.Phone.PhoneNumber(),
                Email = Faker.Internet.Email(),
                Active = true,
                CreatedAt = Faker.Date.Past(),
                CreatedBy = 1,
                UpdatedAt = Faker.Date.Future(),
                UpdatedBy = 1
            };
        }
    }
}
