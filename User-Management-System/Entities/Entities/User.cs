using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
