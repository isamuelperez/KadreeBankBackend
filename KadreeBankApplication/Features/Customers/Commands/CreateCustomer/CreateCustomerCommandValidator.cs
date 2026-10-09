using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;

namespace KadreeBankApplication.Features.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.FullName)
                .NotEmpty()
                .WithMessage("El nombre del cliente es obligatorio.")
                .MaximumLength(150)
                .WithMessage("El nombre no puede superar los 150 caracteres.");

            RuleFor(command => command.Identification)
                .NotEmpty()
                .WithMessage("La identificación es obligatoria.")
                .MaximumLength(30)
                .WithMessage("La identificación no puede superar los 30 caracteres.");

            RuleFor(command => command.Type)
                .IsInEnum()
                .WithMessage("El tipo de cliente no es válido.");
        }
    }
}
