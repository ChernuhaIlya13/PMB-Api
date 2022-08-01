using System;

namespace PMB.Dal.Bll.Dtos
{
    public class KeyDto
    {
        public string Login { get; set; }
        
        public string Key { get; set; }
        
        public DateTimeOffset KeyExpirationTime { get; set; }
        
        public DateTimeOffset? FreezeTime { get; set; }
    }
}