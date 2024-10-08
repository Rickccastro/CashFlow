using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Respositories.Expenses;

public class ExpensesReadOnlyRepositoryBuilder
{
    private readonly Mock<IExpensesReadOnlyRepository> _repository;

    public IExpensesReadOnlyRepository Build()
    {       
        return _repository.Object;
    }

    public ExpensesReadOnlyRepositoryBuilder GetAll(CashFlow.Domain.Entities.User user, List<Expense> expenses)
    {
        _repository.Setup(repository =>  repository.GetAll(user)).ReturnsAsync(expenses);   
     
        return this;
    }

}
