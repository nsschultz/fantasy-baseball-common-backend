using System;
using FantasyBaseball.Common.Exceptions;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Exceptions;

public class BadRequestExceptionTests
{
    [Fact]
    public void Constructor_WithoutArguments_SetsDefaultMessage() =>
      Assert.Equal("The given request was invalid.", new BadRequestException().Message);

    [Fact]
    public void Constructor_WithMessage_SetsMessageProperty()
    {
        var exception = new BadRequestException("message", new Exception("inner message"));
        Assert.Equal("message", exception.Message);
        Assert.Equal("inner message", exception.InnerException.Message);
    }

    [Fact]
    public void Constructor_WithMessageOnly_SetsMessageProperty() =>
      Assert.Equal("message", new BadRequestException("message").Message);
}