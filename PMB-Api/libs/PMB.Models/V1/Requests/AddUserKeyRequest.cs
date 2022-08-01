using System;

namespace PMB.Models.V1.Requests
{
    public class AddUserKeyRequest
    {
        public DateTimeOffset KeyExpirationTime { get; set; }
    }
}