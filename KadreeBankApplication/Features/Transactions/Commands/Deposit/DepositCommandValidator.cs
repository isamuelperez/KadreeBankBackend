using FluentValidation;

namespace KadreeBankApplication.Features.Transactions.Commands.Deposit
{
    public sealed class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(command => command.AccountId)
                .NotEmpty()
                .WithMessage("La cuenta es obligatoria.");

            RuleFor(command => command.Amount)
                .GreaterThan(0)
                .WithMessage("El importe del depósito debe ser mayor que cero.")
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
