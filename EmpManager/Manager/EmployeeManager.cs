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

        public List<EmployeeModel> GetAllEmployee()
        {
            try
            {
                return this.repository.GetAllEmployee();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EmployeeModel GetEmployee(int employeeId)
        {
            try
            {
                return this.repository.GetEmployee(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateEmployeeDetails(EmployeeModel employeemodel)
        {
            try
            {
                return this.repository.UpdateEmployeeDetails(employeemodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
