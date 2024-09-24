
using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class InvalidLoginException : CashFlowException
{
    public InvalidLoginException() : base(ResourceErrorMenssages.EMAIL_OR_PASSWORD_INVALID)
    {
        
    }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
