using EmpModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EmpRepository.Interface
{
    public interface IAddressRepository
    {
        bool AddAddress(AddressModel address);
        List<AddressModel> GetAllAddress();
        AddressModel GetEmployeeAddress(int employeeId);
        bool UpdateEmployeeAddress(AddressModel address);
    }
}