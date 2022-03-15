using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class UserServices : IUserService
    {
        public bool AuthenticateUser(string userName, string password)
        {
            return true;
        }

        public bool RegisterUser(UserModel userModel)
        {
            return true;
        }
    }
}
