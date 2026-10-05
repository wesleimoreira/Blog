namespace Blog.Application.Common.Exceptions
{
    public sealed class ValidationException(Dictionary<string, string[]> errors) : AppException("Validation failed.")
    {
        public Dictionary<string, string[]> Errors { get; } = errors;
    }
}
