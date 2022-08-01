namespace PMB.Dal.Bll.Dtos
{
    public class GetForksByQueryModel
    {
        public string[] Bookmakers { get; set; }
        
        public string[] Sports { get; set; }
        
        public string[] CridIds { get; set; }
        
        public string[] BetTypes { get; set; }
    }
}