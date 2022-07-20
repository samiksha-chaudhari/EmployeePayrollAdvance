using EmpModel;

namespace EmpManager.Interface
{
    public interface IPayoutManager
    {
        bool AddPayout(int salaryID);
        PayoutModel GetEmployeePayoutDetails(int SalaryId);
        bool UpdateEmployeePayout(PayoutModel pay);
    }
}