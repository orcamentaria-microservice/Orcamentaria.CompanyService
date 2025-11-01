using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.CompanyService.Domain.Services;
using Orcamentaria.Lib.Domain.Contexts;
using Orcamentaria.Lib.Domain.Models;

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

        [Authorize(Roles = "MASTER,COMPANY_MASTER,COMPANY:READ")]
        [HttpGet("GetCompany", Name = "CompanyGetCompany")]
        public Response<CompanyResponseDTO> GetCompany()
        {
            try
            {
                return _service.GetById(_userAuthContext.UserCompanyId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER")]
        [HttpGet("GetById/{id}", Name = "CompanyGetById")]
        public Response<CompanyResponseDTO> GetById(long id)
        {
            try
            {
                return _service.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER")]
        [HttpPost(Name = "CompanyInsert")]
        public async Task<Response<CompanyResponseDTO>> Insert([FromBody] CompanyInsertDTO dto)
        {
            try
            {
                return await _service.Insert(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER,COMPANY_MASTER,COMPANY:UPDATE")]
        [HttpGet(Name = "CompanyUpdate")]
        public async Task<Response<CompanyResponseDTO>> Update([FromBody] CompanyUpdateDTO dto)
        {
            try
            {
                return await _service.Update(_userAuthContext.UserCompanyId, dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER")]
        [HttpGet("{id}", Name = "CompanyUpdateWithId")]
        public async Task<Response<CompanyResponseDTO>> Update(long id, [FromBody] CompanyUpdateDTO dto)
        {
            try
            {
                return await _service.Update(id, dto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
