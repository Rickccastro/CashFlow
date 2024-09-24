using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCase.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task<Expense?> GetByID(long id);  
    public Task Execute(long id, RequestExpenseJson request);
}
