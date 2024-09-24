using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Security.Token;

public interface IAcessTokenGenerator
{ 
    string Generate(User user);
}
