using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.Lib.Domain.Models.Responses;

namespace Orcamentaria.CompanyService.Domain.Services
{
    public interface ICompanyService
    {
        Task<Response<CompanyResponseDTO>?> GetByIdAsync(long id);
        Task<Response<IEnumerable<CompanyResponseDTO>>?> GetAsync(GridParams gridParams);
        Task<Response<CompanyResponseDTO>> InsertAsync(CompanyInsertDTO dto);
        Task<Response<CompanyResponseDTO>> UpdateAsync(long id, CompanyUpdateDTO dto);
    }
}
