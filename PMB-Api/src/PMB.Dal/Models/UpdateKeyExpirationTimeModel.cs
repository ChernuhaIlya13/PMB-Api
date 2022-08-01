using System;

namespace PMB.Dal.Models
{
    public class UpdateKeyExpirationTimeModel
    {
        public string Login { get; set; }
        
        public string Key { get; set; }
        
        public DateTimeOffset KeyExpirationTime { get; set; }
    }
}