using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Respositories.Expenses;

public class ExpenseUpdateOnlyRespositoryBuilder
{
    private readonly Mock<IExpensesUpdateOnlyRepository> _repository;

    public ExpenseUpdateOnlyRespositoryBuilder()
    {
        _repository = new Mock<IExpensesUpdateOnlyRepository>();
    }

    public ExpenseUpdateOnlyRespositoryBuilder GetById(CashFlow.Domain.Entities.User user,Expense? expense)
    {
        if(expense is not null)
        {
            _repository.Setup(repository => repository.GetById(user, expense.Id)).ReturnsAsync(expense);
        }

        return this;
    }

    public IExpensesUpdateOnlyRepository Build() 
    {
       return _repository.Object;
    }
}
