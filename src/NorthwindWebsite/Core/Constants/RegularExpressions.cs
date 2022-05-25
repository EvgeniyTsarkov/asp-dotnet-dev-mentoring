namespace NorthwindWebsite.Core.Constants
{
    public static class RegularExpressions
    {
        //Expected result is a positive number of XX.XX format
        public const string Decimal = "^(?:0|[1-9][0-9]*)\\.[0-9]{2}";
    }
}
