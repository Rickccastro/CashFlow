using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Respositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;

    public UserReadOnlyRepositoryBuilder()
    {
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWithEmail(string email)
    {
        _userReadOnlyRepositoryMock.Setup(userReadOnly => userReadOnly.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
    {
        _userReadOnlyRepositoryMock.Setup(userReadOnly => userReadOnly.GetUserByEmail(user.Email)).ReturnsAsync(user);

        return this;
    }

    public IUserReadOnlyRepository Build()
    {
        return _userReadOnlyRepositoryMock.Object;
    }
}
