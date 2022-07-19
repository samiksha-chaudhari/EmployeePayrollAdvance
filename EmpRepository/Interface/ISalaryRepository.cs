using EmpModel;
using Microsoft.Extensions.Configuration;

namespace EmpRepository.Interface
{
    public interface ISalaryRepository
    {
        bool AddSalary(SalaryModel salary);
        SalaryModel GetEmployeeSalaryDetails(int employeeId);
        bool UpdateEmployeeSalary(SalaryModel salary);
    }
}