using System;

namespace PMB.Models.V1.Responses
{
    public class UserKeyResponse
    {
        public string Login { get; set; }
        
        public string Email { get; set; }
        
        public KeyResponse[] Keys { get; set; }
        
        public string[] Roles { get; set; }
        
        public class KeyResponse
        {
            public string Key { get; set; }
        
            public DateTimeOffset KeyExpirationTime { get; set; }
        
            public DateTimeOffset? FreezeTime { get; set; }
        }
    }
}