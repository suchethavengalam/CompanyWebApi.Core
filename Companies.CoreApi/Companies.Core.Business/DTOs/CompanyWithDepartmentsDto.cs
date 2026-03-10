using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Core.Business.DTOs
{
    public class CompanyWithDepartmentsDto(
    int id,
    string name,
    string address,
    string country,
    List<DepartmentDto> departments)
    {
        public int Id { get; init; } = id;

        public string Name { get; init; } = name;

        public string Address { get; init; } = address;

        public string Country { get; init; } = country;

        public List<DepartmentDto> Departments { get; init; } = departments;
    }
   
}
