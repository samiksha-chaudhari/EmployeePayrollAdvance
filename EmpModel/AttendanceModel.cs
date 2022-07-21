using System;
using System.Collections.Generic;
using System.Text;

namespace EmpModel
{
    public class AttendanceModel
    {        
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }       
        public int PresentDay { get; set; }
        public int AbsentDay { get; set; }
        public float DailySalary { get; set; }
    }
}
