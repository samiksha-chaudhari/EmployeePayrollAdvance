using System;
using System.Collections.Generic;
using System.Text;

namespace EmpModel
{
    class PayoutModel
    {      
        public int PayoutId { get; set; }
        public int SalaryId { get; set; }  
        public float CTC { get; set; }
        public float PF { get; set; }
        public float TAX { get; set; }
    }
}
