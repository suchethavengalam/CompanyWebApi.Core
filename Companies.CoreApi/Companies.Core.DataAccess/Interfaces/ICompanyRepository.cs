using System;
using System.Collections.Generic;
using System.Text;
using Companies.Core.DataAccess.Entities;

namespace Companies.Core.DataAccess.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(int id);
        Task<int>Add(Company company);
        Task Update(Company company);
        Task Delete(int id);
        //Task<IEnumerable<Company>> GetAllCompaniesWithDepartments();
        Task<(IEnumerable<Company> Companies, IEnumerable<Department> Departments)> GetAllCompaniesWithDepartments();
    }
}
