using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCase.Login;

public class DoLoginUseCase : IDoLoginUseCase
{

    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAcessTokenGenerator _acessTokenGenerator;


    public DoLoginUseCase(IAcessTokenGenerator acessTokenGenerator, IPasswordEncripter passwordEncripter, IUserReadOnlyRepository repository)
    {
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
        _repository = repository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {

        var user = await _repository.GetUserByEmail(request.Email);
        if (user is null)
        {
            throw new InvalidLoginException();     
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password,user.Password);
        
        if(passwordMatch == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _acessTokenGenerator.Generate(user)
        };
    }
}
