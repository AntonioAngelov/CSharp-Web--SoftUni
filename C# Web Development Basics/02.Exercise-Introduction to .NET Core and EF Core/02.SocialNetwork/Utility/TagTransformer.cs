namespace _02.SocialNetwork.Utility
{
    using System.Text.RegularExpressions;

    public static class TagTransformer
    {
        public static string Transform(string tag)
        {
            string pattern = @"^#([a-zA-Z0-9]{1,19})$";
           
            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(tag))
            {
                return tag;
            }
            else
            {
                if (tag[0] != '#')
                {
                    tag = "#" + tag;
                }

                tag = tag.Replace(" ", string.Empty);

                if (tag.Length > 20)
                {
                    tag.Substring(0, 20);
                }

                return tag;
            }

            
        }
    }
}
