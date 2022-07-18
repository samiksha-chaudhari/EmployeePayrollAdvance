using System;
using System.Collections.Generic;
using System.Text;

namespace EmpModel
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public int EmployeeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
