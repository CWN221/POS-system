namespace Pos_system.Core
{
    public static class PasswordValidator
    {
        public const int MinLength = 8;

        public static OperationResult Validate(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return OperationResult.Fail("Password is required.");

            if (password.Length < MinLength)
                return OperationResult.Fail($"Password must be at least {MinLength} characters.");

            if (!password.Any(char.IsUpper))
                return OperationResult.Fail("Password must contain at least one uppercase letter.");

            if (!password.Any(char.IsLower))
                return OperationResult.Fail("Password must contain at least one lowercase letter.");

            if (!password.Any(char.IsDigit))
                return OperationResult.Fail("Password must contain at least one number.");

            return OperationResult.Ok();
        }
    }
}
