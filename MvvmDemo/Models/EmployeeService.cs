using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MvvmDemo.Models;
namespace MvvmDemo.Models
{
    public class EmployeeService
    {
        MvvmDemoDbEntities ObjContext;
        public EmployeeService()
        {
            ObjContext = new MvvmDemoDbEntities();
        }

        //make some matters
        public List<EmployeeDTO> GetAll()
        {
            List<EmployeeDTO> ObjEmployeesList = new List<EmployeeDTO>();
            try
            {
                var ObjQuery= from employee in ObjContext.Employees
                              select employee;
                foreach (var employee in ObjQuery)
                {
                    ObjEmployeesList.Add(new EmployeeDTO() { Id = employee.Id, Name = employee.Name, Age=employee.Age });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ObjEmployeesList;
        }

        public bool Add(EmployeeDTO employee)
        {
            bool IsAdded=false;
            //Age must be between 21 and 58
            if (employee.Age < 21 || employee.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");

            try
            {
                var ObjEmployee = new Employees();
                ObjEmployee.Id = employee.Id; ObjEmployee.Name = employee.Name; ObjEmployee.Age = employee.Age;

                ObjContext.Employees.Add(ObjEmployee);
                var NoofRowsAffected=ObjContext.SaveChanges();
                IsAdded = NoofRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return IsAdded;
        }

        public bool Update(EmployeeDTO employee)
        {
            bool IsUpdated=false;
            try
            {
                var ObjEmployee=ObjContext.Employees.Find(employee.Id);
                if (ObjEmployee != null)
                {
                    ObjEmployee.Age = employee.Age;
                    ObjEmployee.Name = employee.Name;
                    ObjEmployee.Id= employee.Id;

                    var NoofRowsAffected = ObjContext.SaveChanges();
                    IsUpdated = NoofRowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted=false;
            try
            {
                var ObjEmployee = ObjContext.Employees.Find(id);
                if (ObjEmployee != null)
                {
                    ObjContext.Employees.Remove(ObjEmployee);

                    var NoofRowsAffected = ObjContext.SaveChanges();
                    IsDeleted = NoofRowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return IsDeleted;
        }

        public EmployeeDTO Search(int id)
        {
            EmployeeDTO employee = null;
            try
            {
                var ObjEmployeeToFind=ObjContext.Employees.Find(id);
                if (ObjEmployeeToFind != null)
                {
                    employee = new EmployeeDTO()
                    {
                        Name = ObjEmployeeToFind.Name,
                        Age = ObjEmployeeToFind.Age,
                        Id = ObjEmployeeToFind.Id
                    };

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return employee;
        }
    }
}
