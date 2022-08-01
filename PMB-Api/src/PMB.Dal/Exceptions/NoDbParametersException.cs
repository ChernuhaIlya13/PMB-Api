using System;

namespace PMB.Dal.Exceptions
{
    public class NoDbParametersException: Exception
    {
        public NoDbParametersException() : base("No db parameters found") { }
    }
}