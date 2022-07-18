﻿using EmpModel;
using System.Collections.Generic;

namespace EmpManager.Interface
{
    public interface IEmployeeManager
    {
        bool Register(EmployeeModel EmployeeData);
        List<EmployeeModel> GetAllEmployee();
        EmployeeModel GetEmployee(int employeeId);
        bool UpdateEmployeeDetails(EmployeeModel employeemodel);
    }
}