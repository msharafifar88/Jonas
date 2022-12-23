using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
	    private readonly IDbWrapper<Employee> _employeeDbWrapper;

	    public EmployeeRepository(IDbWrapper<Employee> companyDbWrapper)
	    {
		    _employeeDbWrapper = companyDbWrapper;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeDbWrapper.FindAll();
        }

        public Employee GetByCode(string companyCode)
        {
            return _employeeDbWrapper.Find(t => t.CompanyCode.Equals(companyCode))?.FirstOrDefault();
        }

        public bool SaveEmployee(Employee employee)
        {
            var itemRepo = _employeeDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.EmployeeCode = employee.EmployeeCode;
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.CompanyName = employee.CompanyName;
                itemRepo.OccupationName = employee.OccupationName;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.PhoneNumber = employee.PhoneNumber;
                itemRepo.LastModifiedDateTime = employee.LastModifiedDateTime;
               
                return _employeeDbWrapper.Update(itemRepo);
            }

            return _employeeDbWrapper.Insert(employee);
        }

        public bool AddEmployee(Employee employee)
        {
            return _employeeDbWrapper.Insert(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var itemRepo = _employeeDbWrapper.Find(t =>
               t.SiteId.Equals(id))?.FirstOrDefault();

            if (itemRepo != null)
            {

                return _employeeDbWrapper.Delete(t => string.Equals(t.CompanyCode, id));
            }
            else
                return false;
        }
    }
}
