using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmDemo.Models;
using MvvmDemo.Commands;
namespace MvvmDemo.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        //여러 property를 위한 Helper function을 만든다.
        public void OnChangedProperty(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee=new Employee();
            saveCommand=new RelayCommands(Save);
        }

        #region DisplayOperation
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
        #endregion

        private Employee currentEmployee;

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnChangedProperty("CurrentEmployee"); }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnChangedProperty("Message"); }
        }

        private RelayCommands saveCommand;

        public RelayCommands SaveCommand
        {
            get { return saveCommand; }
        }
        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message="Save operationfailed";
            }
            catch (Exception ex)
            {
                Message=ex.Message;
            }
        }
    }
}
