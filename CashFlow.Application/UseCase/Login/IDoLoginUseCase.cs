using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Login;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson>Execute(RequestLoginJson responseRegisteredUserJson); 
}
