using System.Collections.Generic;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeInfo> GetAllEmployees();
        EmployeeInfo GetEmployeeByCode(string employeeCode);
        bool CreatEmployee(EmployeeInfo employee);
        bool EditEmployee(int id, EmployeeInfo employee);
        bool DeleteEmployee(int id);
        void LoggerErr(object eo, object io, string functionName);
    }
}
