using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Accounts.Commands.OpenAccount
{
    public sealed class OpenAccountCommandValidator : AbstractValidator<OpenAccountCommand>
    {
        public OpenAccountCommandValidator()
        {
            RuleFor(command => command.CustomerId)
                .NotEmpty()
                .WithMessage("El cliente es obligatorio.");

            RuleFor(command => command.AccountNumber)
                .NotEmpty()
                .WithMessage("El número de cuenta es obligatorio.")
                .MaximumLength(30)
                .WithMessage("El número de cuenta no puede superar los 30 caracteres.");

            RuleFor(command => command.Type)
                .IsInEnum()
                .WithMessage("El tipo de cuenta no es válido.");

            RuleFor(command => command.OriginCity)
                .NotEmpty()
                .WithMessage("La ciudad de origen es obligatoria.")
                .MaximumLength(100)
                .WithMessage("La ciudad no puede superar los 100 caracteres.");
        }
    }
}
