using FluentValidation;
using FluentValidation.Results;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.Lib.Domain.Validators;

namespace Orcamentaria.CompanyService.Application.Validators
{
    public class CompanyValidator : AbstractValidator<Company>, IValidatorEntity<Company>
    {
        private readonly ICompanyRepository _repository;

        public CompanyValidator(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public CompanyValidator()
        {
            RuleFor(x => x.CorporateName)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .MaximumLength(90)
                .WithMessage("O tamanho máximo do {PropertyName} é de {MaxLength} caracteres.");
            RuleFor(x => x.TradeName)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .MaximumLength(120)
                .WithMessage("O tamanho máximo do {PropertyName} é de {MaxLength} caracteres.");
            RuleFor(x => x.Cnpj)
                .Length(14).WithMessage("O {PropertyName} deve ter {MaxLength} caracteres.")
                .Matches("^[0-9]+$")
                .When(x => !string.IsNullOrEmpty(x.Cnpj))
                .WithMessage("O {PropertyName} deve conter apenas números.");
            RuleFor(x => x.Address)
                .MaximumLength(150)
                .WithMessage("O tamanho máximo do {PropertyName} é de {MaxLength} caracteres.");
            RuleFor(x => x.TelePhone)
                .MaximumLength(25)
                .WithMessage("O tamanho máximo do {PropertyName} é de {MaxLength} caracteres.");
            RuleFor(x => x.CellPhone)
                .MaximumLength(25)
                .WithMessage("O tamanho máximo do {PropertyName} é de {MaxLength} caracteres.");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("O {PropertyName} não é um email válido.")
                .MaximumLength(100)
                .WithMessage("O tamanho máximo do {PropertyName} é de {MaxLength} caracteres.");

        }

        public ValidationResult ValidateBeforeInsert(Company entity)
        {
            var validator = new CompanyValidator();

            validator.RuleFor(x => x.Id)
                .Empty().WithMessage("O {PropertyName} não deve ser informado.");

            return validator.Validate(entity);
        }

        public ValidationResult ValidateBeforeUpdate(Company entity)
        {
            var validator = new CompanyValidator();

            validator.RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O {PropertyName} deve ser informado.");

            validator.RuleFor(x => x.Id)
               .Must((x, cancelation) =>
               {
                   var entity = _repository.GetById(x.Id);

                   return entity is not null;

               }).WithMessage("Id não encontrado.");

            return validator.Validate(entity);
        }
    }
}
