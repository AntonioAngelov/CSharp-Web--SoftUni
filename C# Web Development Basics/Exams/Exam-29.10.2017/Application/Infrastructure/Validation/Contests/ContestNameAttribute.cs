namespace Application.Infrastructure.Validation.Contests
{
    using System;
    using SimpleMvc.Framework.Attributes.Validation;

    public class ContestNameAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var name = value as string;

            if (name == null)
            {
                return true;
            }

            return Char.IsUpper(name[0])
                   && name.Length >= 3
                   && name.Length <= 100;
        }
    }
}
