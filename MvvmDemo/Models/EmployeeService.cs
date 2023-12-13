using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo.Models
{
    public class EmployeeService
    {
        private static List<Employee> ObjEmployeesList;
        public EmployeeService()
        {
            //List를 초기화해준다.
            ObjEmployeesList = new List<Employee>()
            {
                //예시
                new Employee{Id=101, Name="Syed", Age=25}
            };
        }

        //make some matters
        public List<Employee> GetAll()
        {
            return ObjEmployeesList;
        }

        public bool Add(Employee employee)
        {
            //Age must be between 21 and 58
            if (employee.Age < 21 || employee.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");

            ObjEmployeesList.Add(employee);
            return true;
        }

        public bool Update(Employee employee)
        {
            bool IsUpdated=false;
            for(int index = 0;index<ObjEmployeesList.Count; index++)
            {
                if (ObjEmployeesList[index].Id==employee.Id)
                {
                    ObjEmployeesList[index].Name = employee.Name;
                    ObjEmployeesList[index].Age = employee.Age;
                    IsUpdated = true;
                    break;
                }
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted=false;
            for (int index = 0; index<ObjEmployeesList.Count; index++)
            {
                if (ObjEmployeesList[index].Id == id)
                {
                    ObjEmployeesList.RemoveAt(index);
                    IsDeleted = true;
                    break;
                }
            }
            return IsDeleted;
        }

        public Employee Search(int id)
        {
            return ObjEmployeesList.FirstOrDefault(x => x.Id == id);
        }
    }
}
