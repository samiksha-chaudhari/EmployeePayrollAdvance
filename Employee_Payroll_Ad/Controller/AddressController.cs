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
    }
}
