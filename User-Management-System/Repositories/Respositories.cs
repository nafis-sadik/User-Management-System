using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IUserRepo : IRepositoryBase<User> { }
    public class UserRepo : RepositoryBase<User>, IUserRepo
    {
        public UserRepo(bootcampdb2Context context) : base(context) { }
    }
}
