using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmDemo.Models;
using MvvmDemo.Commands;
using System.Collections.ObjectModel;
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
            searchCommand = new RelayCommands(Search);
            updateCommand = new RelayCommands(Update);
        }

        #region DisplayOperation
        //List를 사용하는 경우는 실시간 업데이트에 동작하지 않는다.
        //List대신 ObservableCollection을 사용한다.
        private ObservableCollection<Employee> employeesList;

        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; OnChangedProperty("EmployeesList"); }
        }

        private void LoadData()
        {
            //ObjEmployeeService에서 반환하는 List도 ObservableCollection형으로 전환하여 전달한다.
            EmployeesList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
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

        #region SaveOperation
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
        #endregion

        #region SearchOperation
        private RelayCommands searchCommand;

        public RelayCommands SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);
                if(ObjEmployee != null)
                {
                    //CurrentEmployee가 업데이트되면 loop가 형성되므로 변경해준다.
                    //CurrentEmployee = ObjEmployee;
                    CurrentEmployee.Name = ObjEmployee.Name;
                    CurrentEmployee.Age = ObjEmployee.Age;
                }
                else
                {
                    Message = "Employee Not found";
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommands updateCommand;

        public RelayCommands UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
                if (IsUpdated)
                {
                    Message = "Employee Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update Operation Failed";
                }
            }
            catch (Exception ex)
            {
                Message=ex.Message;
            }
        }
        #endregion

    }
}
