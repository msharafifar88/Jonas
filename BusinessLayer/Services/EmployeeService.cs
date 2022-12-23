using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Serilog;
using System.IO;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public void LoggerErr(object eo,object io,string functionName)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File($"{Directory.GetCurrentDirectory()}/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Error($@"ERROR HEPPEND {functionName}({io}): ERR:{eo}");
        }

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public IEnumerable<EmployeeInfo> GetAllEmployees()
        {
            var res = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public EmployeeInfo GetEmployeeByCode(string employeeCode)
        {
            var result = _employeeRepository.GetByCode(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public bool CreatEmployee(EmployeeInfo employee)
        {            
            return _employeeRepository.AddEmployee(_mapper.Map<Employee>(employee));            
        }
        public bool EditEmployee(int id,EmployeeInfo employee)
        {
            employee.SiteId = id.ToString();
            return  _employeeRepository.SaveEmployee(_mapper.Map<Employee>(employee));

        }
        public bool DeleteEmployee( int id)
        {

            if (_employeeRepository.DeleteEmployee(id))
                return true;
            else
               return false;
        }

    }
}
