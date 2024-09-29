using CashFlow.Application.UseCase.Login;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Respositories;
using CommonTestUtilities.Token;
using FluentAssertions;
using CommonTestUtilities.Requests.Login;
using CommonTestUtilities.Entities;

namespace UseCases.Tests.Users.Login;

public class DoLoginUseCaseTest
{
    [Fact]  
    public async Task Sucess()
    {
        //Arrange 
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();  
        var useCase = CreateUseCase(user, request.Password);
        request.Email = user.Email;


        //Act
        var result =await useCase.Execute(request);

        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();  
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        //Arrange 
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        var useCase = CreateUseCase(user, request.Password);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(exception => exception.GetErrors().Count == 1 && exception.GetErrors().Contains(ResourceErrorMenssages.EMAIL_OR_PASSWORD_INVALID));

    }


    [Fact]
    public async Task Error_Password_Not_Match()
    {
        //Arrange 
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(exception => exception.GetErrors().Count == 1 && exception.GetErrors().Contains(ResourceErrorMenssages.EMAIL_OR_PASSWORD_INVALID));

    }

    private DoLoginUseCase CreateUseCase(CashFlow.Domain.Entities.User user,string? password = null)
    {  
        var passwordEncripterBuilder = new PasswordEncripterBuilder().Verify(password).Build();
        var jwtTokenGeneratorBuilder = JwtTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        
        return new DoLoginUseCase(jwtTokenGeneratorBuilder, passwordEncripterBuilder, readOnlyRepository);
    }


}
