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

        public string Login(LoginModel login)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("login", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Email", login.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", login.Password);
                    sqlConnection.Open();
                    EmployeeModel register = new EmployeeModel();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.Read())
                    {
                        register.EmployeeId = Convert.ToInt32(sqlData["EmployeeId"]);
                        register.UserName = sqlData["UserName"].ToString();
                        register.Email = sqlData["Email"].ToString();
                        register.MobileNo = sqlData["MobileNo"].ToString();
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "Name", register.EmployeeId);
                        database.StringSet(key: "User Id", register.UserName.ToString());
                        database.StringSet(key: "Number", register.MobileNo.ToString());
                        return "Login Successful";
                    }
                    else
                    {
                        return "Login Unsuccessful";
                    }
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


        //public string Login(LoginModel login)
        //{
        //    sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            SqlCommand sqlCommand = new SqlCommand("spEmployeeLogin", sqlConnection);
        //            // login.Password = EncryptPassword(login.Password);
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //            sqlConnection.Open();
        //            sqlCommand.Parameters.AddWithValue("@Email", login.Email);
        //            sqlCommand.Parameters.AddWithValue("@Password", login.Password);
        //            sqlCommand.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            sqlCommand.ExecuteNonQuery();
        //            var result = sqlCommand.Parameters["@user"].Value;
        //            if (!(result is DBNull))
        //            {
        //                if (Convert.ToInt32(result) == 2)
        //                {
        //                    GetEmployeeByEmail(login.Email);
        //                    return "Login is Successfull";
        //                }
        //                return "Incorrect email or password";
        //            }
        //            return "Login failed";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        public string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public List<EmployeeModel> GetAllEmployee()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllEmp", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<EmployeeModel> EmployeeList = new List<EmployeeModel>();
                        while (reader.Read())
                        {
                            EmployeeModel employeeData = new EmployeeModel();
                            employeeData.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                            employeeData.UserName = reader["UserName"].ToString();
                            employeeData.Email = reader["Email"].ToString();
                            employeeData.Password = reader["Password"].ToString();
                            employeeData.MobileNo = reader["MobileNo"].ToString();
                            employeeData.ProfileImage = reader["ProfileImage"].ToString();
                            employeeData.Gender = reader["Gender"].ToString();
                            employeeData.Department = reader["Department"].ToString();
                            employeeData.StartDate = Convert.ToDateTime(reader["StartDate"]);
                            employeeData.Note = reader["Note"].ToString();

                            EmployeeList.Add(employeeData);
                        }
                        return EmployeeList;
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

        public EmployeeModel GetEmployee(int employeeId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetSpecificEmpDetail", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    EmployeeModel emp = new EmployeeModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        emp.EmployeeId = Convert.ToInt32(read["EmployeeId"]);
                        emp.UserName = read["UserName"].ToString();
                        emp.Email = read["Email"].ToString();
                        emp.Password = read["Password"].ToString();
                        emp.MobileNo = read["MobileNo"].ToString();
                        emp.ProfileImage = read["ProfileImage"].ToString();
                        emp.Gender = read["Gender"].ToString();
                        emp.Department = read["Department"].ToString();
                        emp.StartDate = Convert.ToDateTime(read["StartDate"]);
                        emp.Note = read["Note"].ToString();
                    }
                    return emp;
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

        public EmployeeModel GetEmployeeByEmail(string Email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetSpecificEmpDetailByEmail", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@Email", Email);
                    EmployeeModel emp = new EmployeeModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        emp.EmployeeId = Convert.ToInt32(read["EmployeeId"]);
                        emp.UserName = read["UserName"].ToString();
                        emp.Email = read["Email"].ToString();
                        emp.Password = read["Password"].ToString();
                        emp.MobileNo = read["MobileNo"].ToString();
                        emp.ProfileImage = read["ProfileImage"].ToString();
                        emp.Gender = read["Gender"].ToString();
                        emp.Department = read["Department"].ToString();
                        emp.StartDate = Convert.ToDateTime(read["StartDate"]);
                        emp.Note = read["Note"].ToString();
                    }
                    return emp;
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

        public bool UpdateEmployeeDetails(EmployeeModel employeemodel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateEmployeeDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", employeemodel.EmployeeId);
                    sqlCommand.Parameters.AddWithValue("@UserName", employeemodel.UserName);
                    sqlCommand.Parameters.AddWithValue("@Email", employeemodel.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", employeemodel.Password);
                    sqlCommand.Parameters.AddWithValue("@MobileNo", employeemodel.MobileNo);
                    sqlCommand.Parameters.AddWithValue("@ProfileImage", employeemodel.ProfileImage);
                    sqlCommand.Parameters.AddWithValue("@Gender", employeemodel.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", employeemodel.Department);
                    sqlCommand.Parameters.AddWithValue("@StartDate", employeemodel.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Note", employeemodel.Note);

                    sqlCommand.Parameters.Add("@count", SqlDbType.Int);
                    sqlCommand.Parameters["@count"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = sqlCommand.Parameters["@count"].Value;
                    if (result.Equals(employeemodel.EmployeeId))
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

        public bool Attendance(AttendanceModel attend)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeeDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_EmpAttendance", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", attend.EmployeeId);
                    sqlCommand.Parameters.AddWithValue("@PresentDay", attend.PresentDay);
                    sqlCommand.Parameters.AddWithValue("@AbsentDay", attend.AbsentDay);
                    sqlCommand.Parameters.AddWithValue("@DailySalary", attend.DailySalary);
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

        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key); ////create new instance
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor ////placeholders to store all atrribute to generate token
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

    }
}
