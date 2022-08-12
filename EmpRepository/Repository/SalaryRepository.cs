using EmpModel;
using EmpRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmpRepository.Repository
{
    public class SalaryRepository : ISalaryRepository
    {
        public SalaryRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        public bool AddSalary(SalaryModel salary)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetPresentday", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
                    AttendanceModel attend = new AttendanceModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        attend.PresentDay = Convert.ToInt32(read["PresentDay"]);                       
                        attend.DailySalary = Convert.ToInt32(read["DailySalary"]);
                    }
                    sqlConnection.Close();
                    float amount = attend.PresentDay * attend.DailySalary;

                    SqlCommand sqlCommand1 = new SqlCommand("sp_AddEmpSalary", sqlConnection);
                    sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand1.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
                    sqlCommand1.Parameters.AddWithValue("@SalaryDate", salary.SalaryDate);
                    sqlCommand1.Parameters.AddWithValue("@Amount", amount);
                    sqlCommand1.Parameters.AddWithValue("@PaySlip", salary.PaySlip);
                    int result = sqlCommand1.ExecuteNonQuery();
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

        public List<SalaryModel> GetAllSalary()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllEmpSalary", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<SalaryModel> salaryList = new List<SalaryModel>();
                        while (reader.Read())
                        {
                            SalaryModel Data = new SalaryModel();
                            Data.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                            Data.Amount = Convert.ToInt32(reader["Amount"]);
                            Data.PaySlip = Convert.ToDateTime(reader["PaySlip"]);
                            Data.SalaryDate = Convert.ToDateTime(reader["SalaryDate"]);

                            salaryList.Add(Data);
                        }
                        return salaryList;
                    }
                    else
                    {
                        return null;
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

        public SalaryModel GetEmployeeSalaryDetails(int employeeId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetSpecificEmpSalaryDetail", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    SalaryModel salary = new SalaryModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        salary.EmployeeId = Convert.ToInt32(read["EmployeeId"]);
                        salary.SalaryDate = Convert.ToDateTime(read["SalaryDate"]);
                        salary.Amount = Convert.ToInt32(read["Amount"]);
                        salary.PaySlip = Convert.ToDateTime(read["PaySlip"]);
                    }
                    return salary;
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

        public bool UpdateEmployeeSalary(SalaryModel salary)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateEmployeeSalary", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
                    sqlCommand.Parameters.AddWithValue("@SalaryDate", salary.SalaryDate);
                    sqlCommand.Parameters.AddWithValue("@Amount", salary.Amount);
                    sqlCommand.Parameters.AddWithValue("@PaySlip", salary.PaySlip);

                    sqlCommand.Parameters.Add("@count", SqlDbType.Int);
                    sqlCommand.Parameters["@count"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = sqlCommand.Parameters["@count"].Value;
                    if (result.Equals(salary.EmployeeId))
                        return true;
                    else
                        return false;

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
