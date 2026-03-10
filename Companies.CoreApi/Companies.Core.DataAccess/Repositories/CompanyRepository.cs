using Companies.Core.DataAccess.Entities;
using Companies.Core.DataAccess.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Companies.Core.DataAccess.Repositories
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CompanyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);
        public async Task<IEnumerable<Company>> GetAll()
        {
            using var db = Connection;
            return await db.QueryAsync<Company>(
                "sp_GetCompanies",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Company> GetById(int id)
        {
            using var db = Connection;
#pragma warning disable CS8603 // Possible null reference return.
            return await db.QueryFirstOrDefaultAsync<Company>("sp_GetCompanyById", new { Id = id }, commandType: CommandType.StoredProcedure);
#pragma warning restore CS8603 // Possible null reference return.
        }

       
        public async Task<int> Add(Company company)
        {
            using var db = Connection;
            return await db.ExecuteScalarAsync<int>(
                "sp_AddCompany",
                new
                {
                    company.Name,
                    company.Address,
                    company.Country
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task Update(Company company)
        {
            using var db = Connection;
            await db.ExecuteAsync(
                "sp_UpdateCompany",
                new
                {
                    company.Id,
                    company.Name,
                    company.Address,
                    company.Country
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task Delete(int id)
        {
            using var db = Connection;
            await db.ExecuteAsync(
                "sp_DeleteCompany",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}

