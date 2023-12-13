using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmDemo.Models;
namespace MvvmDemo.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //여러 property를 위한 Helper function을 만든다.
        public void OnChangedProperty(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
        }

        private List<Employee> employeesList;

        public List<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; OnChangedProperty("EmployeesList"); }
        }

        private void LoadData()
        {
            EmployeesList = ObjEmployeeService.GetAll();
        }

    }
}
