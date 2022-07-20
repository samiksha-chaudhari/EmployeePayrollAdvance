using EmpModel;
using Microsoft.Extensions.Configuration;

namespace EmpRepository.Interface
{
    public interface IPayoutRepository
    {
        bool AddPayout(int salaryID);
        PayoutModel GetEmployeePayoutDetails(int employeeId);
        bool UpdateEmployeePayout(PayoutModel pay);
    }
}