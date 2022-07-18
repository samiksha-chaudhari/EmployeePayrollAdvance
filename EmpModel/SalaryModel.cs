using System;
using System.Collections.Generic;
using System.Text;

namespace EmpModel
{
    class SalaryModel
    {        
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }      
        public DateTime SalaryDate { get; set; }
        public float Amount { get; set; }
        public DateTime PaySlip { get; set; }        
    }
}
