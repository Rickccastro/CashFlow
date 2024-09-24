using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Expenses.GetAll;
public interface IGetAllExpenseUseCase
{
    public Task <ResponseExpensesJson> Execute();
}
