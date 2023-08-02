using System.Text;

namespace tmdb.Core.Utilities.Security.Encryption
{
    public static class CustomCryptor
    {
        private const string _divider = "f";
        private const string _mixedReal = "6,(HYtkT\\y%dp-|\"CI.@/z0cNJaEqG'}<xP#uoi`V _MbD5W?+SFm[8eg$>RUKf=2*n)1:X~;{O]4lALZ3jv9^7&!hsrQwB";

        public static string EncryptContent(string? content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < content?.Length; i++)
            {
                result.Append(_mixedReal.IndexOf(content[i]));
                if (i + 1 != content.Length)
                    result.Append(_divider);
            }
            return result.ToString();
        }

        public static string GetDecryptedContent(string? encryptedContent)
        {
            if (string.IsNullOrEmpty(encryptedContent)) return string.Empty;
            StringBuilder result = new StringBuilder();
            var encryptedArr = encryptedContent?.Split(_divider);
            for (int i = 0; i < encryptedArr?.Length; i++)
            {
                int indexOfReal = Convert.ToInt32(encryptedArr[i]);
                result.Append(_mixedReal[indexOfReal]);
            }
            return result.ToString();
        }
    }
}
