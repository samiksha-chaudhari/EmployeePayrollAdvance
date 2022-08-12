using EmpModel;
using System.Collections.Generic;

namespace EmpManager.Interface
{
    public interface IEmployeeManager
    {
        bool Register(EmployeeModel EmployeeData);
        List<EmployeeModel> GetAllEmployee();
        List<AttendanceModel> GetAllEmployeeAttend();
        EmployeeModel GetEmployee(int employeeId);
        EmployeeModel GetEmployeeByEmail(string Email);
        bool UpdateEmployeeDetails(EmployeeModel employeemodel);
        string Login(LoginModel login);
        string GenerateToken(EmployeeModel emp);
        bool Attendance(AttendanceModel attend);
        string LoginAdmin(LoginModel login);
        public string GenerateTokenAdmin(AdminModel admin);
    }
}