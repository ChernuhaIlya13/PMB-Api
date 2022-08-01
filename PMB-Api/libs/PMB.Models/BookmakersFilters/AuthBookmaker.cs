namespace PMB.Models.BookmakersFilters
{
    public class AuthBookmaker
    {
        public bool UseAuthInBk { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public bool ShowImages { get; set; }
    }
}