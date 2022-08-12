using EmpModel;
using System.Collections.Generic;

namespace EmpRepository.Interface
{
    public interface IEmployeeRepository
    {
        bool Register(EmployeeModel EmployeeData);
        List<EmployeeModel> GetAllEmployee();
        EmployeeModel GetEmployee(int employeeId);
        EmployeeModel GetEmployeeByEmail(string Email);
        bool UpdateEmployeeDetails(EmployeeModel employeemodel);
        string Login(LoginModel login);
        string GenerateToken(EmployeeModel emp);
        bool Attendance(AttendanceModel attend);
        List<AttendanceModel> GetAllEmployeeAttend();
        string LoginAdmin(LoginModel login);
        public string GenerateTokenAdmin(AdminModel admin);

    }
}