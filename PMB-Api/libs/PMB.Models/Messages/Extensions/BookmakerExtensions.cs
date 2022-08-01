namespace PMB.Models.Messages.Extensions
{
    public static class BookmakerExtensions
    {
        public static string ToNormalFormat(this BookmakersFilters.Bookmaker bookmaker)
        {
            var bookmakerName = bookmaker.BookmakerName;
            return "_" + bookmakerName.Replace(".", "").Replace("-", "").Replace("_","").Trim();
        }
    }
}