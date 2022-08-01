namespace PMB.Models.BookmakersFilters
{
    public class BrowserOptions
    {
        public Proxy Proxy { get; set; }
        
        public AuthBookmaker Auth { get; set; }
        
        public bool SaveCacheInMemory { get; set; } 
    }
}