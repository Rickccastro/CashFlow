using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Criptography;

public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;


    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();
        _mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("!%kosaks7894");
    }
    public IPasswordEncripter Build()
    {
        return _mock.Object;
    }


    public PasswordEncripterBuilder Verify(string? password)
    {    
        if(string.IsNullOrWhiteSpace(password)==false)
        {
            _mock.Setup(passwordEncripter => passwordEncripter.Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this ; 
    }


}
