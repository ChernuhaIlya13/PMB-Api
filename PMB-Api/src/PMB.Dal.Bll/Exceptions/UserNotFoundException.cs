using System;

namespace PMB.Dal.Bll.Exceptions
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException(string login): base($"User with login {login} not found")
        {
            
        }
    }
}