using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests.Register;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Builder()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }

}
