namespace SoftUniGameStore.Application.Utilities
{
    using System.ComponentModel.DataAnnotations;

    public class GameTitleAttribute : ValidationAttribute
    {
        public GameTitleAttribute()
        {
            this.ErrorMessage = "Game's title must start with a capital letter.";
        }

        public override bool IsValid(object value)
        {
            var title = value as string;
            
            if (string.IsNullOrWhiteSpace(title))
            {
                return false;
            }

            return char.IsUpper(title[0]);
        }
    }
}
