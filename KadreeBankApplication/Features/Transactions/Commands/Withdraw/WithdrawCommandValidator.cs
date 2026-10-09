using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Transactions.Commands.Withdraw
{
    public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(command => command.AccountId)
                .NotEmpty()
                .WithMessage("La cuenta es obligatoria.");

            RuleFor(command => command.Amount)
                .GreaterThan(0)
                .WithMessage("El importe del retiro debe ser mayor que cero.")
                .PrecisionScale(18, 2, ignoreTrailingZeros: true)
                .WithMessage("El importe no puede tener más de dos decimales.");

            RuleFor(command => command.City)
                .NotEmpty()
                .WithMessage("La ciudad de la operación es obligatoria.")
                .MaximumLength(100)
                .WithMessage("La ciudad no puede superar los 100 caracteres.");
        }
    }
}
