using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Login;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Requests.Register;
using CommonTestUtilities.Respositories;
using CommonTestUtilities.Respositories.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Users.Expenses.Register;

public class RegisterExpenseUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var result = await useCase.Execute(request);

        result.Title.Should().NotBeNullOrWhiteSpace();
        result.Title.Should().Be(request.Title);   
    }


    [Fact]
    public async Task Expense_Title_Empty()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterExpenseJsonBuilder.Build();

        request.Title = string.Empty;

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMenssages.TITLE_REQUIRED));
    
    }


    private RegisterExpensesUseCase CreateUseCase(CashFlow.Domain.Entities.User user)
    {
        var repository = ExpensesWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser= LoggedUserBuilder.Build(user);

        return new RegisterExpensesUseCase(repository, unitOfWork, mapper,loggedUser);
    }
}
