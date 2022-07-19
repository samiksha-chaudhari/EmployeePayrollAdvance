using System;
using System.Collections.Generic;
using System.Text;
using EmpManager.Interface;
using EmpModel;
using EmpRepository.Interface;

namespace EmpManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repository;
        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }

        public bool AddAddress(AddressModel address)
        {
            try
            {
                return this.repository.AddAddress(address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
