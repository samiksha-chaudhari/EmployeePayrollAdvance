using EmpModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EmpRepository.Interface
{
    public interface IPayoutRepository
    {
        bool AddPayout(int salaryID);
        List<PayoutModel> GetAllPayout();
        PayoutModel GetEmployeePayoutDetails(int employeeId);
        bool UpdateEmployeePayout(PayoutModel pay);
    }
}