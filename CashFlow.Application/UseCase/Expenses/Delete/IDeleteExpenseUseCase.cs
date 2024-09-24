namespace CashFlow.Application.UseCase.Expenses.Delete;
public interface IDeleteExpenseUseCase 
{
    public Task Execute(long id);

}
