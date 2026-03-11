using Companies.Core.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Core.Business.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyReadDto>> GetCompanies();
        Task<CompanyReadDto> GetCompany(int id);
        Task<int> CreateCompany(CompanyCreateDto dto);
        Task UpdateCompany(CompanyUpdateDto dto);
        Task DeleteCompany(int id);
        Task<IEnumerable<CompanyWithDepartmentsDto>> GetAllCompaniesWithDepartments();
    }
}
