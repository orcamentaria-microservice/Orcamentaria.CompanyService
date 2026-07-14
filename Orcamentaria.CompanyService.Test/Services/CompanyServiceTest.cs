using AutoMapper;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using Moq.AutoMock;
using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Test.Fixtures;
using Orcamentaria.Lib.Domain.Enums;
using Orcamentaria.Lib.Domain.Exceptions;
using Orcamentaria.Lib.Domain.Models.Exceptions;
using Orcamentaria.Lib.Domain.Models.Responses;
using Orcamentaria.Lib.Domain.Validators;
using System.Linq.Expressions;

namespace Orcamentaria.CompanyService.Test.Services
{
    [Collection(nameof(CompanyCollection))]
    public class CompanyServiceTest
    {
        private readonly CompanyFixture _fixture;
        private readonly AutoMocker _mocker;
        private readonly Application.Services.CompanyService _service;

        public CompanyServiceTest(CompanyFixture fixture)
        {
            _fixture = fixture;
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<Application.Services.CompanyService>(true);
        }

        #region GetByIdAsync

        [Xunit.Theory]
        [InlineData(1)]
        public async Task GetByIdAsync_WhenHaveData_ReturnsData(long id)
        {
            var repositoryResponse = _fixture.CreateEntity(id);
            var mapperResponseDTO = new CompanyResponseDTO
            {
                Id = id,
                TradeName = repositoryResponse.TradeName,
                CorporateName = repositoryResponse.CorporateName,
                Cnpj = repositoryResponse.Cnpj,
                Address = repositoryResponse.Address,
                TelePhone = repositoryResponse.TelePhone,
                CellPhone = repositoryResponse.CellPhone,
                Email = repositoryResponse.Email,
                Active = repositoryResponse.Active
            };


            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.GetByIdWithoutCompanyIdAsync(id, It.IsAny<Expression<Func<Company, object>>[]>()))
                .ReturnsAsync(repositoryResponse);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<Company, CompanyResponseDTO>(It.IsAny<Company>()))
                .Returns(mapperResponseDTO);

            var response = await _service.GetByIdAsync(id);

            response.Should().NotBeNull();
            response.Data.Should().BeSameAs(mapperResponseDTO);

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Verify(r => r.GetByIdWithoutCompanyIdAsync(id, It.IsAny<Expression<Func<Company, object>>[]>()), Times.Once());

            _mocker.GetMock<IMapper>()
                .Verify(r => r.Map<Company, CompanyResponseDTO>(It.IsAny<Company>()), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_WhenExceptionOccurs_ThrowsUnexpectedException()
        {
            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.GetByIdWithoutCompanyIdAsync(It.IsAny<long>(), It.IsAny<Expression<Func<Company, object>>[]>()))
                .ThrowsAsync(new Exception());

            Func<Task> act = async () => await _service.GetByIdAsync(1);

            await act.Should().ThrowAsync<UnexpectedException>();
        }

        #endregion

        #region GetAsync

        [Fact]
        public async Task GetAsync_WhenHaveData_ReturnsSuccessTrue()
        {
            var gridParams = _fixture.CreateGridParamsWithWithoutFilter();
            var repositoryResponse = (
                new List<Company>() { new Company { Id = 1 } },
                new ResponsePagination(1, 10, 1)
            );

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.GetAsync(gridParams, It.IsAny<Expression<Func<Company, object>>[]>()))
                .ReturnsAsync(repositoryResponse);


            var response = await _service.GetAsync(gridParams);

            response.Success.Should().BeTrue();
            _mocker.GetMock<ICompanyRepository<Company>>()
                .Verify(r => r.GetAsync(gridParams, It.IsAny<Expression<Func<Company, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_WhenNotHaveData_ThrowsInfoException()
        {
            var gridParams = _fixture.CreateGridParamsWithWithoutFilter();
            var repositoryResponse = (new List<Company>(), new ResponsePagination(1, 10, 0));

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.GetAsync(gridParams, It.IsAny<Expression<Func<Company, object>>[]>()))
                .ReturnsAsync(repositoryResponse);

            Func<Task> act = async () => await _service.GetAsync(gridParams);

            var exception = await act.Should().ThrowAsync<InfoException>();
            exception.Which.ErrorCode.Should().Be((int)ErrorCodeEnum.NotFound);
        }

        #endregion

        #region InsertAsync

        [Fact]
        public async Task InsertAsync_WhenValidBody_ReturnsSuccessTrue()
        {
            var serviceRequest = new CompanyInsertDTO();
            var mapperInsertToEntity = new Company();
            var validationResult = new ValidationResult();
            var repositoryResponse = new Company { Id = 1, TradeName = "test" };
            var mapperResponseDTO = new CompanyResponseDTO();

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<CompanyInsertDTO, Company>(serviceRequest))
                .Returns(mapperInsertToEntity);

            _mocker.GetMock<IValidatorEntity<Company>>()
                .Setup(v => v.ValidateBeforeInsert(mapperInsertToEntity))
                .Returns(validationResult);

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.InsertAsync(mapperInsertToEntity))
                .ReturnsAsync(repositoryResponse);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<Company, CompanyResponseDTO>(repositoryResponse))
                .Returns(mapperResponseDTO);

            var response = await _service.InsertAsync(serviceRequest);

            response.Success.Should().BeTrue();
            _mocker.GetMock<ICompanyRepository<Company>>().Verify(r => r.InsertAsync(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_WhenInvalidBody_ThrowsValidationException()
        {
            var serviceRequest = new CompanyInsertDTO();
            var validationResult = new ValidationResult { Errors = { new ValidationFailure("TradeName", "Error") } };

            _mocker.GetMock<IMapper>().Setup(m => m.Map<CompanyInsertDTO, Company>(serviceRequest)).Returns(new Company());
            _mocker.GetMock<IValidatorEntity<Company>>().Setup(v => v.ValidateBeforeInsert(It.IsAny<Company>())).Returns(validationResult);

            Func<Task> act = async () => await _service.InsertAsync(serviceRequest);

            await act.Should().ThrowAsync<ValidationException>();
        }

        #endregion

        #region UpdateAsync

        [Xunit.Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenValidParameterAndBody_ReturnsSuccessTrue(long id)
        {
            var serviceRequest = new CompanyUpdateDTO();
            var mapperUpdateToEntity = new Company();
            var validationResult = new ValidationResult();
            var repositoryResponse = new Company { Id = id };
            var mapperResponseDTO = new CompanyResponseDTO();

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<CompanyUpdateDTO, Company>(serviceRequest))
                .Returns(mapperUpdateToEntity);

            _mocker.GetMock<IValidatorEntity<Company>>()
                .Setup(v => v.ValidateBeforeUpdate(It.IsAny<Company>()))
                .Returns(validationResult);

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.UpdateAsync(id, mapperUpdateToEntity))
                .ReturnsAsync(repositoryResponse);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<Company, CompanyResponseDTO>(repositoryResponse))
                .Returns(mapperResponseDTO);

            var response = await _service.UpdateAsync(id, serviceRequest);

            response.Success.Should().BeTrue();
            _mocker.GetMock<ICompanyRepository<Company>>().Verify(r => r.UpdateAsync(id, It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenExceptionOccurs_ThrowsUnexpectedException()
        {
            _mocker.GetMock<IMapper>().Setup(m => m.Map<CompanyUpdateDTO, Company>(It.IsAny<CompanyUpdateDTO>())).Throws(new Exception());

            Func<Task> act = async () => await _service.UpdateAsync(1, new CompanyUpdateDTO());

            await act.Should().ThrowAsync<UnexpectedException>();
        }

        #endregion
    }
}
