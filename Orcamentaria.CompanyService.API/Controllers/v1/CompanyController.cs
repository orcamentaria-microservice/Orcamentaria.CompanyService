using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.CompanyService.Domain.Services;
using Orcamentaria.Lib.Domain.Contexts;
using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.Lib.Domain.Models.Responses;

namespace Orcamentaria.CompanyService.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;
        private readonly IUserAuthContext _userAuthContext;

        public CompanyController(
            ICompanyService service, 
            IUserAuthContext userAuthContext)
        {
            _service = service;
            _userAuthContext = userAuthContext;
        }

        [Authorize(Roles = "MASTER")]
        [HttpGet("GetMaster", Name = "CompanyGetOnlyMaster")]
        public async Task<Response<IEnumerable<CompanyResponseDTO>>?> GetAsync(GridParams gridParams)
        {
            try
            {
                return await _service.GetAsync(gridParams);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER,COMPANY_MASTER,COMPANY:READ")]
        [HttpGet("GetCompany", Name = "CompanyGetById")]
        public async Task<Response<CompanyResponseDTO>?> GetByIdAsync()
        {
            try
            {
                return await _service.GetByIdAsync(_userAuthContext.CompanyId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER")]
        [HttpPost("Insert", Name = "CompanyInsertOnlyMaster")]
        public async Task<Response<CompanyResponseDTO>> Insert([FromBody] CompanyInsertDTO dto)
        {
            try
            {
                return await _service.InsertAsync(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER,COMPANY_MASTER,COMPANY:UPDATE")]
        [HttpGet("Update", Name = "CompanyUpdate")]
        public async Task<Response<CompanyResponseDTO>> Update([FromBody] CompanyUpdateDTO dto)
        {
            try
            {
                return await _service.UpdateAsync(_userAuthContext.CompanyId, dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER")]
        [HttpGet("Update/{id}", Name = "CompanyUpdateOnlyMaster")]
        public async Task<Response<CompanyResponseDTO>> Update(long id, [FromBody] CompanyUpdateDTO dto)
        {
            try
            {
                return await _service.UpdateAsync(id, dto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
