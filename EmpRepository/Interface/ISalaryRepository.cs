using EmpModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EmpRepository.Interface
{
    public interface ISalaryRepository
    {
        bool AddSalary(SalaryModel salary);
        List<SalaryModel> GetAllSalary();
        SalaryModel GetEmployeeSalaryDetails(int employeeId);
        bool UpdateEmployeeSalary(SalaryModel salary);
    }
}