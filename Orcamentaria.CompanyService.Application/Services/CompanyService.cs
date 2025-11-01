using AutoMapper;
using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Domain.Services;
using Orcamentaria.Lib.Domain.Enums;
using Orcamentaria.Lib.Domain.Exceptions;
using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.Lib.Domain.Models.Exceptions;
using Orcamentaria.Lib.Domain.Validators;

namespace Orcamentaria.CompanyService.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IValidatorEntity<Company> _validator;
        private readonly IMapper _mapper;

        public CompanyService(
            ICompanyRepository repository, 
            IValidatorEntity<Company> validator,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public Response<CompanyResponseDTO> GetById(long id)
        {
            try
            {
                var data = _repository.GetById(id);

                if (data is null)
                    throw new InfoException($"Nenhum dado foi encontrado", ErrorCodeEnum.NotFound);

                return new Response<CompanyResponseDTO>(_mapper.Map<Company, CompanyResponseDTO>(data));
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }

        public async Task<Response<CompanyResponseDTO>> Insert(CompanyInsertDTO dto)
        {
            try
            {
                var company = _mapper.Map<CompanyInsertDTO, Company>(dto);

                var result = _validator.ValidateBeforeInsert(company);

                if (!result.IsValid)
                    throw new ValidationException(result);

                var entity = await _repository.Insert(company);

                return new Response<CompanyResponseDTO>(_mapper.Map<Company, CompanyResponseDTO>(entity));
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }

        public async Task<Response<CompanyResponseDTO>> Update(long id, CompanyUpdateDTO dto)
        {
            try
            {
                var company = _mapper.Map<CompanyUpdateDTO, Company>(dto);

                company.Id = id;

                var result = _validator.ValidateBeforeUpdate(company);

                if (!result.IsValid)
                    throw new ValidationException(result);

                var entity = await _repository.Update(id, company);

                return new Response<CompanyResponseDTO>(_mapper.Map<Company, CompanyResponseDTO>(entity));
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }
    }
}
