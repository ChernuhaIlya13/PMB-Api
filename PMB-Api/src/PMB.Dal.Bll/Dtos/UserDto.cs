using System;
using PMB.Models.V1.Enums;

namespace PMB.Dal.Bll.Dtos
{
    public class UserDto
    {
        public string Login { get; set; }
        
        public string Email { get; set; }
        
        public KeyDto[] Keys { get; set; }
        
        public RoleDto[] Roles { get; set; }
        
        public class KeyDto
        {
            public string Key { get; set; }
        
            public DateTimeOffset KeyExpirationTime { get; set; }
        
            public DateTimeOffset? FreezeTime { get; set; }
        }
        
        public class RoleDto
        {
            public string Login { get; set; }
        
            public Role Role { get; set; }
        }
    }
}