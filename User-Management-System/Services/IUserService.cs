using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IUserService
    {
        public bool AuthenticateUser(string userName, string password);
        public bool RegisterUser(UserModel userModel);
    }
}
