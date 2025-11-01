using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.Lib.Domain.Models;

namespace Orcamentaria.CompanyService.Domain.Services
{
    public interface ICompanyService
    {
        Response<CompanyResponseDTO> GetById(long id);
        Task<Response<CompanyResponseDTO>> Insert(CompanyInsertDTO dto);
        Task<Response<CompanyResponseDTO>> Update(long id, CompanyUpdateDTO dto);
    }
}
