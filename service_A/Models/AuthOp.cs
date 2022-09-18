using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace service_A.Models
{
    public class AuthOp
    {
        const string KEY = "mysupersecret_secretkey!123";   
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
