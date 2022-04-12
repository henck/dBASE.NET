using System;

namespace dBASE.NET.Tests;

public class MyEqualException : Xunit.Sdk.EqualException
{
    public MyEqualException(object expected, object actual, string userMessage)
        : base(expected, actual)
    {
        UserMessage = userMessage;
    }

    public override string Message => $"{UserMessage}{Environment.NewLine}{base.Message}";
}