using EmpManager.Interface;
using EmpModel;
using EmpRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpManager.Manager
{
    public class PayoutManager : IPayoutManager
    {
        private readonly IPayoutRepository repository;
        public PayoutManager(IPayoutRepository repository)
        {
            this.repository = repository;
        }

        public bool AddPayout(int salaryID)
        {
            try
            {
                return this.repository.AddPayout(salaryID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PayoutModel GetEmployeePayoutDetails(int SalaryId)
        {
            try
            {
                return this.repository.GetEmployeePayoutDetails(SalaryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateEmployeePayout(PayoutModel pay)
        {
            try
            {
                return this.repository.UpdateEmployeePayout(pay);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

