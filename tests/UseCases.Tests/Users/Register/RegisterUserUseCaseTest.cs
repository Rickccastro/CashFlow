using CashFlow.Application.UseCase.User.Register;
using FluentAssertions;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Respositories;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Token;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using CommonTestUtilities.Requests.Register;


namespace UseCases.Tests.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        //Arrange 
        var request = RequestRegisterUserJsonBuilder.Builder();
        var useCase = CreateUseCase();

        //Act
        var result = await useCase.Execute(request);

        //Assert       
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace(); 
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        //Arrange 
        var request = RequestRegisterUserJsonBuilder.Builder();
        request.Name = string.Empty;

        var useCase = CreateUseCase();  

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exception => exception.GetErrors().Count == 1 && exception.GetErrors().Contains(ResourceErrorMenssages.NAME_EMPTY));
    }


    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        //Arrange 
        var request = RequestRegisterUserJsonBuilder.Builder();
        var useCase = CreateUseCase(request.Email);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exception => exception.GetErrors().Count == 1 && exception.GetErrors().Contains(ResourceErrorMenssages.EMAIL_ALREADY_EXISTS));
    }



    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build(); 
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripterBuilder = new PasswordEncripterBuilder().Build();
        var jwtTokenGeneratorBuilder = JwtTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder();

        if(string.IsNullOrEmpty(email) == false)
        {
            readOnlyRepository.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(mapper, passwordEncripterBuilder, readOnlyRepository.Build(), unitOfWork, writeOnlyRepository, jwtTokenGeneratorBuilder);   
    }
}
