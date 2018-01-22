namespace _02.SocialNetwork.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValidPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valuesAsString = value as string;
            if (valuesAsString == null)
            {
                return true;
            }

            char[] specialSymbols =
            {
                '!', '@', '#', '$',
                '%', '^', '&', '*',
                '(', ')', '_', '+',
                '<', '>', '?'
            };

            this.ErrorMessage = "The provided password is not valid!";

            return valuesAsString.Any(c => char.IsLower(c))
                   && valuesAsString.Any(c => char.IsUpper(c))
                   && valuesAsString.Any(c => char.IsDigit(c))
                   && valuesAsString.Any(c => specialSymbols.Any(s => s == c));
        }
    }
}
