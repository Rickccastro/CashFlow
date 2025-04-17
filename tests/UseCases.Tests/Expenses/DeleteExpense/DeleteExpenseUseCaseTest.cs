using CashFlow.Application.UseCase.Expenses.Delete;
using CashFlow.Application.UseCase.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Login;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Respositories;
using CommonTestUtilities.Respositories.Expenses;
using FluentAssertions;
using PdfSharp.Drawing;

namespace UseCases.Tests.Expenses.DeleteExpense;

public class DeleteExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);
        var useCase = CreateUserCase(loggedUser, expense);

        var act = async () => await useCase.Execute(expense.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]  
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUserCase(loggedUser);


        // FORMA MENOS SIMPLIFICADA.
       /*
            Func<Task> act = async delegate
            {
                await useCase.Execute(id: 1000);
            };
       */

        var act = async () => await useCase.Execute(id:1000);


        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMenssages.EXPENSE_NOT_FOUND));
    }

    private DeleteExpenseUseCase CreateUserCase(User user, Expense? expense = null)
    {
        var repositoryWriteOnly = ExpensesWriteOnlyRepositoryBuilder.Build();
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new DeleteExpenseUseCase(repositoryWriteOnly, unitOfWork, loggedUser, repository);
    }
}
