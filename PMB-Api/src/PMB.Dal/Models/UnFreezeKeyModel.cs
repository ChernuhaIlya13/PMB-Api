using System;

namespace PMB.Dal.Models
{
    public class UnFreezeKeyModel
    {
        public string Login { get; set; }
        
        public string Key { get; set; }
        
        public DateTimeOffset KeyExpirationTime { get; set; }
    }
}