using EmpModel;
using Microsoft.Extensions.Configuration;

namespace EmpRepository.Interface
{
    public interface IAddressRepository
    {
       bool AddAddress(AddressModel address);
    }
}