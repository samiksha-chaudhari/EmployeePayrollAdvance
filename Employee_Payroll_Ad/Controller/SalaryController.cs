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
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryManager manager;
        public SalaryController(ISalaryManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddSalary")]
        public IActionResult AddSalary([FromBody] SalaryModel salary)            
        {
            try
            {
                var result = this.manager.AddSalary(salary);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Salary Added Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add Salary, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetEmployeeSalaryDetails")]
        public IActionResult GetEmployeeSalaryDetails(int employeeId)
        {
            var result = this.manager.GetEmployeeSalaryDetails(employeeId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "SAlary is retrived", data = result });
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
        [Route("api/UpdateEmployeeSalary")]
        public IActionResult UpdateEmployeeSalary(SalaryModel salary)
        {
            try
            {
                var result = this.manager.UpdateEmployeeSalary(salary);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Employee Salary updated Successfully !" });
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
