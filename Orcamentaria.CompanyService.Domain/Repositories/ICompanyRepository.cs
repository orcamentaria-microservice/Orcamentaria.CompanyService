using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.Lib.Domain.Models.Responses;
using System.Linq.Expressions;

namespace Orcamentaria.CompanyService.Domain.Repositories
{
    public interface ICompanyRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdWithoutCompanyIdAsync(long id, params Expression<Func<TEntity, object>>[] includes);
        Task<(IEnumerable<TEntity?>, ResponsePagination pagination)> GetAsync(GridParams gridParams, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(long id, TEntity entity);
    }
}
