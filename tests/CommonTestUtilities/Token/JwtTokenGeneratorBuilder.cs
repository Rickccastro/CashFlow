using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Token;
using Moq;

namespace CommonTestUtilities.Token;

public class JwtTokenGeneratorBuilder
{
    public static IAcessTokenGenerator Build()
    {
        var mock = new Mock<IAcessTokenGenerator>();

        mock.Setup(acessTokenGenerator => acessTokenGenerator.Generate(It.IsAny<User>())).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        return mock.Object;
    }
}
