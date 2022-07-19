using EmpModel;
using EmpRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmpRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        public bool AddAddress(AddressModel address)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_AddEmpAddress", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", address.EmployeeId);
                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
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
