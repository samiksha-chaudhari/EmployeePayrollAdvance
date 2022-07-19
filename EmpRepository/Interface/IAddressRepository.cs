using EmpModel;
using Microsoft.Extensions.Configuration;

namespace EmpRepository.Interface
{
    public interface IAddressRepository
    {
        bool AddAddress(AddressModel address);
        AddressModel GetEmployeeAddress(int employeeId);
        bool UpdateEmployeeAddress(AddressModel address);
    }
}