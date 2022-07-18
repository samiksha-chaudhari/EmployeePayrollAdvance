using EmpModel;
using System.Collections.Generic;

namespace EmpRepository.Interface
{
    public interface IEmployeeRepository
    {
        bool Register(EmployeeModel EmployeeData);
        List<EmployeeModel> GetAllEmployee();
        EmployeeModel GetEmployee(int employeeId);
        bool UpdateEmployeeDetails(EmployeeModel employeemodel);

    }
}