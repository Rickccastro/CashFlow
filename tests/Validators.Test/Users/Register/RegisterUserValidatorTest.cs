using CashFlow.Application.UseCase.User.Register;
using CashFlow.Exception;
using FluentAssertions;
using Validators.Test.Expenses.Register;

namespace Validators.Test.Users.Register;

public class RegisterUserValidatorTest
{

    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Builder();

        //Act 

        var result = validator.Validate(request);


        //Assert

        result.IsValid.Should().BeTrue();   

    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Builder();

        request.Name = name;

        //Act 

        var result = validator.Validate(request);


        //Assert

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(errors=> errors.ErrorMessage.Equals(ResourceErrorMenssages.NAME_EMPTY));  

    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Builder();

        request.Email = email;

        //Act 

        var result = validator.Validate(request);


        //Assert
        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMenssages.EMAIL_EMPTY));

    }

    [Fact]
    public void Error_Email_Invalid()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Builder();

        request.Email = "Ricardo.com";

        //Act 

        var result = validator.Validate(request);


        //Assert

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMenssages.EMAIL_INVALID));

    }


    [Fact]
    public void Error_Password_Empty()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Builder();

        request.Password = string.Empty;

        //Act 

        var result = validator.Validate(request);


        //Assert

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMenssages.INVALID_PASSWORD));
    }
}
