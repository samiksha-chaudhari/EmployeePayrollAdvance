using EmpManager.Interface;
using EmpModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New User Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new user, Try again" });
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
                    return this.Ok(new { Status = true, Message = "All Notes", data = result });
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
                    return this.Ok(new { Status = true, Message = "Book is retrived", data = result });
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
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book updated Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to updated Book" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
