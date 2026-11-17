using System;
using FantasyBaseball.Common.Exceptions;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Exceptions;

public class ServiceExceptionTests
{
    [Fact]
    public void Constructor_WithoutArguments_SetsDefaultMessage() =>
      Assert.Equal("There was an unknown exception with the service.", new ServiceException().Message);

    [Fact]
    public void Constructor_WithMessage_SetsMessageProperty()
    {
        var exception = new ServiceException("message", new Exception("inner message"));
        Assert.NotNull(exception.InnerException);
        Assert.Equal("message", exception.Message);
        Assert.Equal("inner message", exception.InnerException.Message);
    }

    [Fact]
    public void Constructor_WithMessageOnly_SetsMessageProperty() =>
      Assert.Equal("message", new ServiceException("message").Message);
}