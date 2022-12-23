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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;


        public void LoggerErr(object eo,object io,string functionName)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File($"{Directory.GetCurrentDirectory()}/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Error($@"ERROR HEPPEND {functionName}({io}): ERR:{eo}");
        }

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public bool CreatCompany(CompanyInfo company)
        {            
            return _companyRepository.AddCompany(_mapper.Map<Company>(company));            
        }
        public bool EditCompany(int id,CompanyInfo company)
        {
            company.SiteId = id.ToString();
            return _companyRepository.SaveCompany(_mapper.Map<Company>(company));

        }
        public bool DeleteCompany( int id)
        {

            if (_companyRepository.DeleteCompany(id))
                return true;
            else
                return false;
        }

    }
}
