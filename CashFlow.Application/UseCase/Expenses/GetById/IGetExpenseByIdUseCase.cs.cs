using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Expenses.GetById;
public interface IGetExpenseByIdUseCase
{
    public Task<ResponseExpenseJson> Execute(long id);

}
