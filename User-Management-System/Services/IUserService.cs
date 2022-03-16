using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IUserService
    {
        public string AuthenticateUser(string userName, string password);
        public bool RegisterUser(UserModel userModel);
    }
}
