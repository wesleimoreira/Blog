namespace Blog.Application.Common.Exceptions
{
    public abstract class AppException(string message) : Exception(message)
    {
    }
}
