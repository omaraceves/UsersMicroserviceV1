using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public Guid WatchLaterId { get; set; }
    }
}
