namespace PMB.Dal.Extensions
{
    public static class BookmakerExtensions
    {
        public static string ToNormalFormat(this string bookmaker)
        {
            return "_" + bookmaker.Replace(".", "").Replace("-", "").Replace("_","").Trim();
        }
    }
}