

namespace UserLogin.App_Code.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Security.Cryptography;
    public class User
    {
        string name;
        string password;
        public string hashpass()
        {
            return MD5.Create(password).ToString();
        }
    }
}
