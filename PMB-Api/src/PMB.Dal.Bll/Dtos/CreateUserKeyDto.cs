using System;

namespace PMB.Dal.Bll.Dtos
{
    public class CreateUserKeyDto
    {
        public string Login { get; set; }
        
        public DateTimeOffset KeyExpirationTime { get; set; }
    }
}