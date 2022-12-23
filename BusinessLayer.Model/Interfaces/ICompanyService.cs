using System.Collections.Generic;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        bool CreatCompany(CompanyInfo company);

        bool EditCompany(int id, CompanyInfo company);

        bool DeleteCompany(int id);
        void LoggerErr(object eo, object io, string functionName);
    }
}
