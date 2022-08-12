using EmpModel;
using System.Collections.Generic;

namespace EmpManager.Interface
{
    public interface IAddressManager
    {
        bool AddAddress(AddressModel address);
        List<AddressModel> GetAllAddress();
        AddressModel GetEmployeeAddress(int employeeId);
        bool UpdateEmployeeAddress(AddressModel address);
    }
}