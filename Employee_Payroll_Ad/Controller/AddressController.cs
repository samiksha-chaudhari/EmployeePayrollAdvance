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
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;
        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddAddress")]
        public IActionResult AddAddress([FromBody] AddressModel address)           
        {
            try
            {
                var result = this.manager.AddAddress(address);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Address Added Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new address, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetEmployeeAddress")]
        public IActionResult GetEmployeeAddress(int employeeId)
        {
            var result = this.manager.GetEmployeeAddress(employeeId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Address is retrived", data = result });
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
        [Route("api/UpdateEmployeeAddress")]
        public IActionResult UpdateEmployeeAddress(AddressModel address)
        {
            try
            {
                var result = this.manager.UpdateEmployeeAddress(address);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Employee Address updated Successfully !" });
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
