using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests.Login;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(login => login.Email, faker => faker.Internet.Email())
            .RuleFor(login => login.Password, faker => faker.Internet.Password(prefix: "!Aa1"));

    }
}
