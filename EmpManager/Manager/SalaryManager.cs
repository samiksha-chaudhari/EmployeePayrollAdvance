using EmpManager.Interface;
using EmpModel;
using EmpRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpManager.Manager
{
    public class SalaryManager : ISalaryManager
    {
        private readonly ISalaryRepository repository;
        public SalaryManager(ISalaryRepository repository)
        {
            this.repository = repository;
        }

        public bool AddSalary(SalaryModel salary)
        {
            try
            {
                return this.repository.AddSalary(salary);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SalaryModel> GetAllSalary()
        {
            try
            {
                return this.repository.GetAllSalary();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SalaryModel GetEmployeeSalaryDetails(int employeeId)
        {
            try
            {
                return this.repository.GetEmployeeSalaryDetails(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateEmployeeSalary(SalaryModel salary)
        {
            try
            {
                return this.repository.UpdateEmployeeSalary(salary);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
