using EmpModel;

namespace EmpManager.Interface
{
    public interface IAddressManager
    {
        bool AddAddress(AddressModel address);
        AddressModel GetEmployeeAddress(int employeeId);
        bool UpdateEmployeeAddress(AddressModel address);
    }
}