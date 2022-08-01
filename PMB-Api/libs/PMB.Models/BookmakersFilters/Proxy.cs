namespace PMB.Models.BookmakersFilters
{
    public class Proxy
    {
        public bool UseProxy { get; set; }
        
        public Schemes Schema { get; set; }
        
        public string IpAdress { get; set; }
        
        public int Port { get; set; }
        
        public bool NeedAuthProxy { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}