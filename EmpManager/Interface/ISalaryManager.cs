using EmpModel;
using System.Collections.Generic;

namespace EmpManager.Interface
{
    public interface ISalaryManager
    {
        bool AddSalary(SalaryModel salary);
        List<SalaryModel> GetAllSalary();
        SalaryModel GetEmployeeSalaryDetails(int employeeId);
        bool UpdateEmployeeSalary(SalaryModel salary);
    }
}