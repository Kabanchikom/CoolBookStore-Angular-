namespace Web.Areas.Account.Exceptions;

public class SignInException : Exception
{
    public SignInException(string? message) : base(message)
    {
    }
}