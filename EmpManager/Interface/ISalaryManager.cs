using EmpModel;

namespace EmpManager.Interface
{
    public interface ISalaryManager
    {
        bool AddSalary(SalaryModel salary);
        SalaryModel GetEmployeeSalaryDetails(int employeeId);
        bool UpdateEmployeeSalary(SalaryModel salary);
    }
}