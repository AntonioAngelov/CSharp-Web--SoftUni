namespace GameStore.App.Infrastructure.Validation.Users
{
    using System;
    using System.Linq;
    using SimpleMvc.Framework.Attributes.Validation;
    public class PasswordAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
            {
                return true;
            }

            return password.Length >= 6
                   && password.Any(ch => Char.IsLower(ch))
                   && password.Any(ch => Char.IsUpper(ch))
                   && password.Any(ch => Char.IsDigit(ch));
        }
    }
}
