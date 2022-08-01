using System;

namespace PMB.Dal.Models
{
    public class FreezeKeyModel
    {
        public string Login { get; set; }
        
        public string Key { get; set; }
        
        public DateTimeOffset FreezeTime { get; set; }
    }
}