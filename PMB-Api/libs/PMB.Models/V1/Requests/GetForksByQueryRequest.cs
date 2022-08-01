namespace PMB.Models.V1.Requests
{
    public class GetForksByQueryRequest
    {
        public string[] Bookmakers { get; set; }
        
        public string[] Sports { get; set; }
        
        public string[] CridIds { get; set; }
        
        public string[] BetTypes { get; set; }
    }
}