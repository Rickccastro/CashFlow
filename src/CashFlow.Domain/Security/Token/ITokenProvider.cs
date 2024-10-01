namespace CashFlow.Domain.Security.Token;

public interface ITokenProvider
{
    string TokenOnRequest();
}
