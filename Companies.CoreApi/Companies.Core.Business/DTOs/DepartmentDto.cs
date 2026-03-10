using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Core.Business.DTOs
{
    public class DepartmentDto(int id, int companyId, string name, string description)
    {
        public int Id { get; init; } = id;

        public int CompanyId { get; init; } = companyId;

        public string Name { get; init; } = name;

        public string Description { get; init; } = description;


    }
}
