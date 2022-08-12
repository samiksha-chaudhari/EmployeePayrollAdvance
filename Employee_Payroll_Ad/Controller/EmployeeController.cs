using EmpManager.Interface;
using EmpModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Payroll_Ad.Controller
{
    [AllowAnonymous]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager manager;
        public EmployeeController(IEmployeeManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] EmployeeModel EmployeeData)
        {
            try
            {
                var result = this.manager.Register(EmployeeData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New Employee Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new Employee, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Employee)]
        [HttpPost]
        [Route("api/Employeelogin")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            try 
            {
                var result = this.manager.Login(login);
                if (result.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string UserName = database.StringGet("UserName");
                    int EmployeeId = Convert.ToInt32(database.StringGet("EmployeeId"));
                    string MobileNo = database.StringGet("MobileNo");
                    EmployeeModel data = new EmployeeModel
                    {
                        UserName = UserName,
                        Email = login.Email,
                        EmployeeId = EmployeeId,
                        MobileNo = MobileNo
                    };
                    string token = this.manager.GenerateToken(data);
                    return this.Ok(new { Status = true, Message = result, Data = data, Token = token });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [Authorize (Roles=Role.Admin)]
        [HttpPost]
        [Route("api/Adminlogin")]
        public IActionResult LoginAdmin([FromBody] LoginModel login)
        {
            try
            {
                var result = this.manager.LoginAdmin(login);
                if (result.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string AdminName = database.StringGet("AdminName");
                    int AdminId = Convert.ToInt32(database.StringGet("AdminId"));
                    string MobileNo = database.StringGet("MobileNo");
                    AdminModel data = new AdminModel
                    {
                        AdminName = AdminName,
                        Email = login.Email,
                        AdminId = AdminId,
                        MobileNo = MobileNo
                    };
                    string token = this.manager.GenerateTokenAdmin(data);
                    return this.Ok(new { Status = true, Message = result, Data = data, Token = token });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            var result = this.manager.GetAllEmployee();
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "All Employeee Details", data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("api/GetEmployee")]
        public IActionResult GetEmployee(int employeeId)
        {
            var result = this.manager.GetEmployee(employeeId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Employee details retrived", data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("api/GetEmployeeByEmail")]
        public IActionResult GetEmployeeByEmail(string Email)
        {
            var result = this.manager.GetEmployeeByEmail(Email);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Employee details retrived", data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
        [HttpPut]
        [Route("api/UpdateEmployeeDetails")]
        public IActionResult UpdateEmployeeDetails(EmployeeModel employeemodel)
        {
            try
            {
                var result = this.manager.UpdateEmployeeDetails(employeemodel);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Employee Details updated Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to updated Details" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Attendance")]
        public IActionResult Attendance([FromBody] AttendanceModel attend)
        {
            try
            {
                var result = this.manager.Attendance(attend);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added Employee Attendance Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new Employee, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetAllEmployeeAttend")]
        public IActionResult GetAllEmployeeAttend()
        {
            var result = this.manager.GetAllEmployeeAttend();
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "All Employeee Details", data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
    }
}
