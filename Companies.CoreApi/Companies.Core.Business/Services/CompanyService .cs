using Companies.Core.Business.DTOs;
using Companies.Core.Business.Interfaces;
using Companies.Core.DataAccess.Entities;
using Companies.Core.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Core.Business.Services
{
    public class CompanyService(ICompanyRepository _repository, IEmailService emailService,
    ILogger<CompanyService> logger) : ICompanyService
    {
        public async Task<IEnumerable<CompanyReadDto>> GetCompanies()
        {
            var companies = await _repository.GetAll();

            return companies.Select(c => new CompanyReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Country = c.Country
            });
        }

        public async Task<CompanyReadDto> GetCompany(int id)
        {
            var c = await _repository.GetById(id);
            if (c == null) return null;

            return new CompanyReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Country = c.Country
            };
        }



        public async Task<int> CreateCompany(CompanyCreateDto dto)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Creating company {Name}", dto.Name);
            }
            var entity = new Company
            {
                Name = dto.Name,
                Address = dto.Address,
                Country = dto.Country
            };

            var id = await _repository.Add(entity);

            if (id > 0)
                await emailService.SendCompanyCreatedEmail(entity.Name);
            if (logger.IsEnabled(LogLevel.Information))
            {

                logger.LogInformation("Inserted company with Id {Id}", id);
            }
            return id;
        }
        public async Task UpdateCompany(CompanyUpdateDto dto)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Updating company {Name}", dto.Name);
            }
            var company = new Company
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Country = dto.Country
            };

            await _repository.Update(company);
            await emailService.SendCompanyUpdatedEmail(company.Name);
        }

        public async Task DeleteCompany(int id)
            => await _repository.Delete(id);

        //public async Task<IEnumerable<CompanyWithDepartmentsDto>> GetAllCompaniesWithDepartments()
        //{
        //    var companies = await _repository.GetAllCompaniesWithDepartments();

        //    return companies.Select(c => new CompanyWithDepartmentsDto(
        //        c.Id,
        //        c.Name,
        //        c.Address,
        //        c.Country,
        //        [.. c.Departments.Select(d =>
        //            new DepartmentDto(
        //                d.Id,
        //                d.CompanyId,
        //                d.Name,
        //                d.Description
        //            ))]
        //    ));
        //}
        //public async Task<IEnumerable<CompanyWithDepartmentsDto>> GetAllCompaniesWithDepartments()
        //{
        //    var (companies, departments) = await _repository.GetAllCompaniesWithDepartments();

        //    var departmentLookup = departments
        //        .GroupBy(d => d.CompanyId)
        //        .ToDictionary(g => g.Key, g => g.ToList());

        //    return companies.Select(c =>
        //    {
        //        departmentLookup.TryGetValue(c.Id, out var deptList);

        //        return new CompanyWithDepartmentsDto(
        //            c.Id,
        //            c.Name,
        //            c.Address,
        //            c.Country,
        //            deptList?.Select(d => new DepartmentDto(d.Id, d.CompanyId, d.Name, d.Description)).ToList()
        //            ?? new List<DepartmentDto>()
        //        );
        //    });
        //}
        public async Task<IEnumerable<CompanyWithDepartmentsDto>> GetAllCompaniesWithDepartments()
        {
            var (companies, departments) = await _repository.GetAllCompaniesWithDepartments();

            // Use a lookup dictionary for better performance (O(n) instead of O(n²))
            var departmentLookup = departments
                .GroupBy(d => d.CompanyId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var companyDtos = companies.Select(c =>
            {
                departmentLookup.TryGetValue(c.Id, out var deptList);

                return new CompanyWithDepartmentsDto(
                    c.Id,
                    c.Name,
                    c.Address,
                    c.Country,
                    deptList?.Select(d => new DepartmentDto(d.Id, d.CompanyId, d.Name, d.Description)).ToList()
                    ?? []
                );
            });

            return companyDtos;
        }
    }
}

