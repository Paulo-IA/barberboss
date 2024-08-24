using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Billings.Register;

public class RegisterBillingValidatorTests
{

    [Fact]
    public void Success()
    {
        // Arrange -> Criar instâncias de tudo que precisa pra rodar o teste
        var validator = new RegisterBillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        // Arrange
        var validator = new RegisterBillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.Title = title;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result
            .Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        // Arrange
        var validator = new RegisterBillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.Date = DateTime.Now.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result
            .Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.BILLING_CANT_BE_FOR_THE_FUTURE));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        // Arrange
        var validator = new RegisterBillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result
            .Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void Error_Amount_Invalid(decimal amount)
    {
        // Arrange
        var validator = new RegisterBillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result
            .Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_AMOUNT_MUST_BE_GREATHER_THAN_ZERO));
    }
}
