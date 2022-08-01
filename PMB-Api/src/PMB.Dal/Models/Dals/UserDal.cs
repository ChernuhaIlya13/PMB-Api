using System.Collections.Generic;

namespace PMB.Dal.Models.Dals
{
    public class UserDal
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<KeyDal> Keys { get; set; } = new();

        public List<UserRoleDal> Roles { get; set; } = new();
    }
}