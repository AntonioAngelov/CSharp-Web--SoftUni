namespace GameStore.App.Infrastructure.Validation.Games
{
    using SimpleMvc.Framework.Attributes.Validation;

    public class TitleAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var title = value as string;

            if (title == null)
            {
                return true;
            }

            return char.IsUpper(title[0])
                   && title.Length >= 3
                   && title.Length <= 100;
        }
    }
}
