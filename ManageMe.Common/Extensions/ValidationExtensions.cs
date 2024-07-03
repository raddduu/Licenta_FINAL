using FluentValidation.Results;
using SocializR.Common.Exceptions;

namespace SocializR.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThenThrow(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result);
            }
        }
    }
}
