using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using Newtonsoft.Json;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Serilog;
using BusinessLayer.Model.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
             
        }

        // GET api/<controller>/5
        //[Route("test/{companyCode}")]
       // [HttpGet(Name="GetCompany")]
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }
        [HttpPost]
        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody]string value)
        {
            try
            {
                //throw new Exception("testttt");
                CompanyDto companyDto = Newtonsoft.Json.JsonConvert.DeserializeObject<CompanyDto>(value);
                CompanyInfo company = _mapper.Map<CompanyInfo>(companyDto);
                if (_companyService.CreatCompany(company))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception e)
            {

                _companyService.LoggerErr(e, value, "CompanyController.Post");
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
        }
        [HttpPut]
        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> Put(int id, [FromBody]string value)
        {
            try
            {
                CompanyDto companyDto = Newtonsoft.Json.JsonConvert.DeserializeObject<CompanyDto>(value);
                CompanyInfo company = _mapper.Map<CompanyInfo>(companyDto);
                if(_companyService.EditCompany(id, company))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception e)
            {
                _companyService.LoggerErr(e, value, "CompanyController.Put");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                if (_companyService.DeleteCompany(id))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                _companyService.LoggerErr(e, id, "CompanyController.Put");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        
        }
    }
}