using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace NorthwindWebsite.Core.Utils
{
    public static class StringUtils
    {
        public static string EncodeCode(string code) => 
            WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    }
}
