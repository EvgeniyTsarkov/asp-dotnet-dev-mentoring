using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace NorthwindWebsite.Presentation.Utils
{
    public static class WebUtils
    {
        public static string EncodeToWeb(string code) =>
            WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        public static string DecodeFromWeb(string code) =>
            Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
    }
}
