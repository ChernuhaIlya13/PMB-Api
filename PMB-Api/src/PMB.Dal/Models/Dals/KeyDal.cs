using System;

namespace PMB.Dal.Models.Dals
{
    public class KeyDal
    {
        public string Login { get; set; }
        
        public string Key { get; set; }
        
        public DateTimeOffset KeyExpirationTime { get; set; }
        
        public DateTimeOffset? FreezeTime { get; set; }
        
    }
}