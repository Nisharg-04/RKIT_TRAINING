using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthDemo.App_Start
{
    public class JwtConfig
    {
        public static string SecretKey = "THIS_IS_SUPER_SECRET_KEY_12345_ABCDEFGHIJKLMNOPWXYZ";
        public static string Issuer = "DemoIssuer";
        public static string Audience = "DemoAudience";
    }
}