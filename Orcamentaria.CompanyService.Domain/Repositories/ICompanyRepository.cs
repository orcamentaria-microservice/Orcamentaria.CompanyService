using Orcamentaria.CompanyService.Domain.Models;

namespace Orcamentaria.CompanyService.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Company GetById(long id);
        Task<Company> Insert(Company company);
        Task<Company> Update(long id, Company company);
    }
}
