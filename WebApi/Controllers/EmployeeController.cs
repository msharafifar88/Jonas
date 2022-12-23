using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Serilog;
using BusinessLayer.Model.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService EmployeeService, IMapper mapper)
        {

            _employeeService = EmployeeService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>>  GetAll()
        {
            var items = _employeeService.GetAllEmployees();
            var tt = _mapper.Map<IEnumerable<EmployeeDto>>(items);
            return tt;
        }

   
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var item = _employeeService.GetEmployeeByCode(employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }
        [HttpPost]
        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody]string value)
        {
            try
            {
             
                EmployeeDto employeeDto = Newtonsoft.Json.JsonConvert.DeserializeObject<EmployeeDto>(value);
                EmployeeInfo employee = _mapper.Map<EmployeeInfo>(employeeDto);
                if (_employeeService.CreatEmployee(employee))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception e)
            {

                _employeeService.LoggerErr(e, value, "EmployeeController.Post");
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
        }
        [HttpPut]
        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> Put(int id, [FromBody]string value)
        {
            try
            {
            EmployeeDto employeeDto = Newtonsoft.Json.JsonConvert.DeserializeObject<EmployeeDto>(value);
            EmployeeInfo employee = _mapper.Map<EmployeeInfo>(employeeDto);
                if (_employeeService.EditEmployee(id, employee))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {

                _employeeService.LoggerErr(e, value, "EmployeeController.Post");
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
        }
        [HttpDelete]
        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                if (_employeeService.DeleteEmployee(id))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                _employeeService.LoggerErr(e, id, "EmployeeController.Put");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
    }
}