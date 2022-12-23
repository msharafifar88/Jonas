using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetByCode(string employeeCode);
        bool SaveEmployee(Employee employee);
        bool AddEmployee(Employee employee);
        bool DeleteEmployee(int id);
    }
}
