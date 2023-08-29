namespace Hotel.Helpers
{
    public class StringHelper
    {
        public static string BosluklariSil(string str)
        {
            str = str.Trim();
            var newStr = string.Empty;

            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ' && str[i + 1] == ' ')
                {
                    continue;

                }
                newStr += str[i];
            }
            return newStr;
        }
    }
}
