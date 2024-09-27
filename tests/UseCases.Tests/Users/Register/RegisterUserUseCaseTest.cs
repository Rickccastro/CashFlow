using CashFlow.Application.UseCase.User.Register;
using FluentAssertions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Respositories;

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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build(); 
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();  

        return new RegisterUserUseCase(mapper, null, null, unitOfWork, userWriteOnlyRepository, null);   
    }
}
