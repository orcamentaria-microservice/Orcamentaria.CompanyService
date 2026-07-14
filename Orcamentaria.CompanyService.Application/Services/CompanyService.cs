using AutoMapper;
using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Domain.Services;
using Orcamentaria.Lib.Domain.Enums;
using Orcamentaria.Lib.Domain.Exceptions;
using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.Lib.Domain.Models.Exceptions;
using Orcamentaria.Lib.Domain.Models.Responses;
using Orcamentaria.Lib.Domain.Validators;

namespace Orcamentaria.CompanyService.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository<Company> _repository;
        private readonly IValidatorEntity<Company> _validator;
        private readonly IMapper _mapper;

        public CompanyService(
            ICompanyRepository<Company> repository, 
            IValidatorEntity<Company> validator,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Response<CompanyResponseDTO>?> GetByIdAsync(long id)
        {
            try
            {
                var data = await _repository.GetByIdWithoutCompanyIdAsync(id);

                if (data is null)
                    throw new InfoException($"Nenhum dado foi encontrado.", ErrorCodeEnum.NotFound);

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

        public async Task<Response<IEnumerable<CompanyResponseDTO>>?> GetAsync(GridParams gridParams)
        {
            try
            {
                var (data, pagination) = await _repository.GetAsync(gridParams);

                if (!data.Any())
                    throw new InfoException($"Nenhum dado foi encontrado.", ErrorCodeEnum.NotFound);

                return new Response<IEnumerable<CompanyResponseDTO>>(
                        data.Select(x => _mapper.Map<Company, CompanyResponseDTO>(x)), pagination);
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

        public async Task<Response<CompanyResponseDTO>> InsertAsync(CompanyInsertDTO dto)
        {
            try
            {
                var company = _mapper.Map<CompanyInsertDTO, Company>(dto);

                var result = _validator.ValidateBeforeInsert(company);

                if (!result.IsValid)
                    throw new ValidationException(result);

                var entity = await _repository.InsertAsync(company);

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

        public async Task<Response<CompanyResponseDTO>> UpdateAsync(long id, CompanyUpdateDTO dto)
        {
            try
            {
                var company = _mapper.Map<CompanyUpdateDTO, Company>(dto);

                company.Id = id;

                var result = _validator.ValidateBeforeUpdate(company);

                if (!result.IsValid)
                    throw new ValidationException(result);

                var entity = await _repository.UpdateAsync(id, company);

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
