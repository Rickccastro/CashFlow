using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException : CashFlowException
{
    private readonly List<string> _errors;

    public ErrorOnValidationException(List<string> errorMessagens) : base(string.Empty)
    {
        _errors = errorMessagens;
    }
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return _errors; 
    }

    
}
