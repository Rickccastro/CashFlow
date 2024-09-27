using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Respositories;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();

        return mock.Object;
    }
}
