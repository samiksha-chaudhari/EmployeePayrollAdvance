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

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            try
            {

                string result = this.manager.Login(login);
                if (result.Equals("Login is Successfull"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();

                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add("EmployeeId", database.StringGet("EmployeeId"));
                    data.Add("UserName", database.StringGet("UserName"));
                    data.Add("MobileNo", database.StringGet("MobileNo"));
                    data.Add("Email", login.Email);
                    data.Add("accessToken", this.manager.GenerateToken(login.Email));
                    return this.Ok(new { Status = true, Message = result, result = data });
                }
                else if (result.Equals("Invalid Password"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
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
    }
}
