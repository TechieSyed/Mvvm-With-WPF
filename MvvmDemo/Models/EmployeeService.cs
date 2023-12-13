using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace MvvmDemo.Models
{
    public class EmployeeService
    {
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;
        public EmployeeService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnection"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure ;
        }

        //make some matters
        public List<Employee> GetAll()
        {
            List<Employee> ObjEmployeesList = new List<Employee>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectAllEmployees";

                ObjSqlConnection.Open();
                var ObjSqlDataReader=ObjSqlCommand.ExecuteReader();
                if(ObjSqlDataReader.HasRows)
                {
                    Employee ObjEmployee = null;
                    while(ObjSqlDataReader.Read())
                    {
                        ObjEmployee = new Employee();
                        ObjEmployee.Id = ObjSqlDataReader.GetInt32(0);
                        ObjEmployee.Name = ObjSqlDataReader.GetString(1);
                        ObjEmployee.Age = ObjSqlDataReader.GetInt32(2);

                        ObjEmployeesList.Add(ObjEmployee);
                    }
                }
                ObjSqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            { 
                ObjSqlConnection.Close();
            }

            return ObjEmployeesList;
        }

        public bool Add(Employee employee)
        {
            bool IsAdded=false;
            //Age must be between 21 and 58
            if (employee.Age < 21 || employee.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_InsertEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", employee.Age);

                ObjSqlConnection.Open();
                int NoofRowsAffected=ObjSqlCommand.ExecuteNonQuery();
                IsAdded = NoofRowsAffected> 0;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            { 
                ObjSqlConnection.Close( );
            }
            return IsAdded;
        }

        public bool Update(Employee employee)
        {
            bool IsUpdated=false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_UpdateEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", employee.Age);

                ObjSqlConnection.Open();
                int NoofRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsUpdated = NoofRowsAffected > 0;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted=false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_DeleteEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();
                int NoofRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsDeleted = NoofRowsAffected > 0;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsDeleted;
        }

        public Employee Search(int id)
        {
            Employee employee = null;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectEmployeeById";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();
                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if(ObjSqlDataReader.HasRows)
                {
                    ObjSqlDataReader.Read();
                    employee= new Employee();
                    employee.Id = ObjSqlDataReader.GetInt32(0);
                    employee.Name = ObjSqlDataReader.GetString(1);
                    employee.Age = ObjSqlDataReader.GetInt32(2);
                }
                ObjSqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return employee;
        }
    }
}
