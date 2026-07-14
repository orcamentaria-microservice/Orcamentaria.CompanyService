using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Orcamentaria.CompanyService.Application.Validators;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Infrastructure.Repositories;
using Orcamentaria.CompanyService.Test.Fixtures;
using Xunit;

namespace Orcamentaria.CompanyService.Test.Validators
{
    [Collection(nameof(CompanyCollection))]
    public class CompanyValidatorTest
    {
        private readonly CompanyFixture _fixture;
        private readonly AutoMocker _mocker;
        private readonly CompanyValidator _validator;

        public CompanyValidatorTest(CompanyFixture fixture)
        {
            _fixture = fixture;
            _mocker = new AutoMocker();
            _validator = _mocker.CreateInstance<CompanyValidator>(true);
        }

        #region ValidateBeforeInsert

        [Fact]
        public void ValidateBeforeInsert_WhenEntityValid_ReturnsIsValid()
        {
            var entity = _fixture.CreateEntity(0);
            entity.Cnpj = "12345678901234";
            entity.Email = "teste@teste.com";

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void ValidateBeforeInsert_WhenIdIsProvided_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(1);

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Id não deve ser informado.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenCorporateNameIsEmpty_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.CorporateName = string.Empty;

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Corporate Name é obrigatório.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenCorporateNameExceedsMaxLength_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.CorporateName = new string('A', 91);

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O tamanho máximo do Corporate Name é de 90 caracteres.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenTradeNameIsEmpty_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.TradeName = string.Empty;

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Trade Name é obrigatório.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenTradeNameExceedsMaxLength_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.TradeName = new string('A', 121);

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O tamanho máximo do Trade Name é de 120 caracteres.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenCnpjIsInvalid_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.Cnpj = "abc";

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Cnpj deve ter 14 caracteres.");
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Cnpj deve conter apenas números.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenAddressExceedsMaxLength_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.Address = new string('A', 151);

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O tamanho máximo do Address é de 150 caracteres.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenTelePhoneExceedsMaxLength_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.TelePhone = new string('1', 26);

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O tamanho máximo do Tele Phone é de 25 caracteres.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenCellPhoneExceedsMaxLength_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.CellPhone = new string('1', 26);

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O tamanho máximo do Cell Phone é de 25 caracteres.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenEmailIsInvalid_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.Email = "email-invalido";

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Email não é um email válido.");
        }

        [Fact]
        public void ValidateBeforeInsert_WhenEmailExceedsMaxLength_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);
            entity.Email = new string('a', 95) + "@a.com";

            var result = _validator.ValidateBeforeInsert(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O tamanho máximo do Email é de 100 caracteres.");
        }

        #endregion

        #region ValidateBeforeUpdate

        [Fact]
        public void ValidateBeforeUpdate_WhenEntityValid_ReturnsIsValid()
        {
            var entity = _fixture.CreateEntity(1);
            entity.Cnpj = "12345678901234";
            entity.Email = "teste@teste.com";

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.GetByIdWithoutCompanyIdAsync(entity.Id))
                .ReturnsAsync(new Company());

            var result = _validator.ValidateBeforeUpdate(entity);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void ValidateBeforeUpdate_WhenIdIsEmpty_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(0);

            var result = _validator.ValidateBeforeUpdate(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "O Id deve ser informado.");
        }

        [Fact]
        public void ValidateBeforeUpdate_WhenIdNotFound_ReturnsInvalidAndErrorMessage()
        {
            var entity = _fixture.CreateEntity(1);

            _mocker.GetMock<ICompanyRepository<Company>>()
                .Setup(r => r.GetByIdWithoutCompanyIdAsync(entity.Id))
                .ReturnsAsync((Company)null);

            var result = _validator.ValidateBeforeUpdate(entity);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Id não encontrado.");
        }

        #endregion
    }
}