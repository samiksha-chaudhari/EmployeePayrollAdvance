using System;
using System.Collections.Generic;
using System.Text;

namespace EmpModel
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string ProfileImage { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public string Note { get; set; }
        public string serviceType { get; } = "Employee";
        public string Token { get; set; }
    }
}
