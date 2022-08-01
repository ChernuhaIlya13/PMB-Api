using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PMB.Dal.Bll.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "PMB"; 
        public const string AUDIENCE = "Default"; 
        const string KEY = "mysupersecret_secretkey!123";   
        public const int LIFETIME = 60; 
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(KEY));
        
    }
}