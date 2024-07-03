using FluentValidation.Results;

namespace SocializR.Common.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public readonly ValidationResult ValidationResult;

        public ValidationErrorException(ValidationResult result)
        {
            ValidationResult = result;
        }
    }
}
