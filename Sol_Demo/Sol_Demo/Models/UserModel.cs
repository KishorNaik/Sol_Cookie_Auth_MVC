using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Models
{
    public class UserModel
    {
        public int? Id { get; set; }

        public String FullName { get; set; }

        public String Role { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String Token { get; set; }
    }
}