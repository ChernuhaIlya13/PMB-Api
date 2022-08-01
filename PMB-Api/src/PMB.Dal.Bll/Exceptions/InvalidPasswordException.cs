using System;

namespace PMB.Dal.Bll.Exceptions
{
    public class InvalidPasswordException: Exception
    {
        public InvalidPasswordException(): base("Invalid password")
        {
            
        }
    }
}