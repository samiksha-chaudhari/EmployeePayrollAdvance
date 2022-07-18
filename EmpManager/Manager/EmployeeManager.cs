using EmpModel;
using EmpRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using EmpManager.Interface;

namespace EmpManager.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository repository;
        public EmployeeManager(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public bool Register(EmployeeModel EmployeeData)
        {
            try
            {
                return this.repository.Register(EmployeeData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
