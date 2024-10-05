using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Respositories.User;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();

        return mock.Object;
    }
}
