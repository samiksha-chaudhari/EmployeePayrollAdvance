using EmpModel;
using System.Collections.Generic;

namespace EmpManager.Interface
{
    public interface IPayoutManager
    {
        bool AddPayout(int salaryID);
        List<PayoutModel> GetAllPayout();
        PayoutModel GetEmployeePayoutDetails(int SalaryId);
        bool UpdateEmployeePayout(PayoutModel pay);
    }
}