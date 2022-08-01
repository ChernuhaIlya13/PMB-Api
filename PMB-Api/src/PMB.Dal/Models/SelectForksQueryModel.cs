namespace PMB.Dal.Models
{
    public class SelectForksQueryModel
    {
        public string[] Bookmakers { get; set; }
        
        public string[] Sports { get; set; }
        
        public string[] CridIds { get; set; }
        
        public string[] BetTypes { get; set; }
    }
}