namespace GameStore.App.Infrastructure.Validation.Users
{
    using SimpleMvc.Framework.Attributes.Validation;

    public class EmailAttribute : PropertyValidationAttribute

    {
        public override bool IsValid(object value)
        {
            var emailAsString = value as string;

            if (emailAsString == null)
            {
                return true;
            }

            return emailAsString.Contains("@")
                   && emailAsString.Contains(".");
        }
    }
}
