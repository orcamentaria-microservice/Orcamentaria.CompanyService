using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Infrastructure.Contexts;
using Orcamentaria.Lib.Domain.Exceptions;

namespace Orcamentaria.CompanyService.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly MySqlContext _dbContext;

        public CompanyRepository(MySqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Company GetById(long id)
        {
            try
            {
                return _dbContext.Companies.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
        }

        public async Task<Company> Insert(Company company)
        {
            try
            {
                _dbContext.Companies.Add(company);
                await _dbContext.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
        }

        public async Task<Company> Update(long id, Company company)
        {
            try
            {
                var entity = _dbContext.Companies.First(p => p.Id == id);

                entity.CorporateName = company.CorporateName;
                entity.TradeName = company.TradeName;
                entity.Cnpj = company.Cnpj;
                entity.Address = company.Address;
                entity.TelePhone = company.TelePhone;
                entity.CellPhone = company.CellPhone;
                entity.Email = company.Email;
                entity.Active = company.Active;

                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
        }
    }
}
