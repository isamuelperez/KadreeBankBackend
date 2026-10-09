using MediatR;

namespace KadreeBankApplication.Features.Transactions.Commands.Withdraw
{
    public sealed record WithdrawCommand(
        Guid AccountId,
        decimal Amount,
        string City
    ) : IRequest<Guid>;
}
