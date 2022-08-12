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
    public class PayoutController : ControllerBase
    {
        private readonly IPayoutManager manager;
        public PayoutController(IPayoutManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddPayout")]
        public IActionResult AddPayout(int salaryID)
        {
            try
            {
                var result = this.manager.AddPayout(salaryID);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Payout Added Successfully !" });
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
        [Route("api/GetAllPayout")]
        public IActionResult GetAllPayout()
        {
            var result = this.manager.GetAllPayout();
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "All Employeee Payout Details", data = result });
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
        [Route("api/GetEmployeePayoutDetails")]
        public IActionResult GetEmployeePayoutDetails(int SalaryId)
        {
            var result = this.manager.GetEmployeePayoutDetails(SalaryId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Payout is retrived", data = result });
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
        [Route("api/UpdateEmployeePayout")]
        public IActionResult UpdateEmployeePayout(PayoutModel pay)
        {
            try
            {
                var result = this.manager.UpdateEmployeePayout(pay);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Employee payout updated Successfully !" });
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
