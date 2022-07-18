using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EmpModel;
using EmpRepository.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace EmpRepository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        public bool Register(EmployeeModel EmployeeData)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_EmpRegister", sqlConnection);
                    //userData.Password = EncryptPassword(userData.Password);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();                   
                    sqlCommand.Parameters.AddWithValue("@UserName", EmployeeData.UserName);
                    sqlCommand.Parameters.AddWithValue("@Email", EmployeeData.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", EmployeeData.MobileNo);
                    sqlCommand.Parameters.AddWithValue("@MobileNo", EmployeeData.Password);
                    sqlCommand.Parameters.AddWithValue("@ProfileImage", EmployeeData.ProfileImage);
                    sqlCommand.Parameters.AddWithValue("@Gender", EmployeeData.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", EmployeeData.Department);
                    sqlCommand.Parameters.AddWithValue("@startDate", EmployeeData.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Note", EmployeeData.Note);
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
