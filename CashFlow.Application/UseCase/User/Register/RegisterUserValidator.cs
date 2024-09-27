using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace CashFlow.Application.UseCase.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMenssages.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMenssages.EMAIL_EMPTY)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email ) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMenssages.EMAIL_INVALID);

         RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}

