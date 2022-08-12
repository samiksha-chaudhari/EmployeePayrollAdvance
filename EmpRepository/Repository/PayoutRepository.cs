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
    public class PayoutRepository : IPayoutRepository
    {
        public PayoutRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        public bool AddPayout(int salaryID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetSalaryAmt", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@SalaryId", salaryID);
                    SalaryModel salary = new SalaryModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        salary.Amount = Convert.ToInt32(read["Amount"]);
                    }
                    sqlConnection.Close();
                    float ctc = salary.Amount * 12;
                    float pf = (ctc * 10) / 100;
                    float tax = (ctc * 5) / 100;

                    SqlCommand sqlCommand1 = new SqlCommand("sp_AddEmpPayout", sqlConnection);
                    sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand1.Parameters.AddWithValue("@SalaryId", salaryID);
                    sqlCommand1.Parameters.AddWithValue("@CTC", ctc);
                    sqlCommand1.Parameters.AddWithValue("@PF", pf);
                    sqlCommand1.Parameters.AddWithValue("@TAX", tax);
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

        public List<PayoutModel> GetAllPayout()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllEmpPay", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.HasRows)
                    {
                        List<PayoutModel> PayoutList = new List<PayoutModel>();
                        while (read.Read())
                        {
                            PayoutModel pay = new PayoutModel();
                            pay.SalaryId = Convert.ToInt32(read["SalaryId"]);
                            pay.CTC = (float)Convert.ToDouble(read["CTC"]);
                            pay.PF = (float)Convert.ToDouble(read["PF"]);
                            pay.TAX = (float)Convert.ToDouble(read["TAX"]);

                            PayoutList.Add(pay);
                        }
                        return PayoutList;
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

        public PayoutModel GetEmployeePayoutDetails(int SalaryId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetSpecificEmpPayoutDetail", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@SalaryId", SalaryId);
                    PayoutModel pay = new PayoutModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        pay.SalaryId = Convert.ToInt32(read["SalaryId"]);
                        pay.CTC = (float)Convert.ToDouble(read["CTC"]);
                        pay.PF = (float)Convert.ToDouble(read["PF"]);
                        pay.TAX = (float)Convert.ToDouble(read["TAX"]);
                    }
                    return pay;
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

        public bool UpdateEmployeePayout(PayoutModel pay)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateEmployeePayout", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@SalaryId", pay.SalaryId);
                    sqlCommand.Parameters.AddWithValue("@CTC", pay.CTC);
                    sqlCommand.Parameters.AddWithValue("@PF", pay.PF);
                    sqlCommand.Parameters.AddWithValue("@TAX", pay.TAX);

                    sqlCommand.Parameters.Add("@count", SqlDbType.Int);
                    sqlCommand.Parameters["@count"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = sqlCommand.Parameters["@count"].Value;
                    if (result.Equals(pay.SalaryId))
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
