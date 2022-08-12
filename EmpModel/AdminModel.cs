using System;
using System.Collections.Generic;
using System.Text;

namespace EmpModel
{
    public class AdminModel
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string serviceType { get; } = "Admin";
        public string Token { get; set; }
    }
}
