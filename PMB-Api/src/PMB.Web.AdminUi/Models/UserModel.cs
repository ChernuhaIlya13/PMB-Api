using System;
using System.Linq;
using PMB.Admin.Domain;

namespace PMB.Web.AdminUi.Models
{
    public class UserModel
    {
        public string Login { get; set; }

        public long KeysCount { get; set; }

        public bool Expanded { get; set; }
        
        public KeyModel[] Keys { get; set; }
        
        public class KeyModel
        {
            public string Login { get; set; }
            
            public string Key { get; set; }
            
            public DateTimeOffset KeyExpirationTime { get; set; }
            
            public DateTimeOffset? FreezeTime { get; set; }

            public bool IsFreeze { get; set; }
        }
    }

    public static class UserModelExtensions
    {
        public static UserModel Convert(this UserQueryResult result)
        {
            return new UserModel
            {
                Expanded = false,
                Login = result.Login,
                KeysCount = result.KeysCount,
                Keys = result.Keys?.Select(x => x.Convert()).ToArray()
            };
        }

        public static UserModel.KeyModel Convert(this KeyResult result)
        {
            return new UserModel.KeyModel
            {
                Key = result.Key,
                Login = result.Login,
                KeyExpirationTime = result.KeyExpirationTime,
                FreezeTime = result.FreezeTime,
                IsFreeze = result.FreezeTime.HasValue
            };
        }
    }
}