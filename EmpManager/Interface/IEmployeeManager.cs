using EmpModel;
using System.Collections.Generic;

namespace EmpManager.Interface
{
    public interface IEmployeeManager
    {
        bool Register(EmployeeModel EmployeeData);
        List<EmployeeModel> GetAllEmployee();
        EmployeeModel GetEmployee(int employeeId);
        EmployeeModel GetEmployeeByEmail(string Email);
        bool UpdateEmployeeDetails(EmployeeModel employeemodel);
        string Login(LoginModel login);
        string GenerateToken(string userName);
    }
}