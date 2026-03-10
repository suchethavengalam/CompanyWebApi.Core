using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Companies.Core.DataAccess;
using Companies.Core.Business;
using Companies.Core.DataAccess.Entities;
using Companies.Core.Business.DTOs;
using Companies.Core.Business.Interfaces;
using Microsoft.Extensions.Options;


namespace Companies.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyService _service,IOptions< EmailSettings> emailSettings) : ControllerBase
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        [HttpGet]
        public async Task<ActionResult<CompanyReadDto>> Get()
             => Ok(await _service.GetCompanies());

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyReadDto>> Get(int id)
        {
            var company = await _service.GetCompany(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyCreateDto>> Post(CompanyCreateDto dto)
        {
          var id=  await _service.CreateCompany(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<ActionResult<CompanyUpdateDto>> Put(CompanyUpdateDto dto)
        {
            await _service.UpdateCompany(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyReadDto>> Delete(int id)
        {
            await _service.DeleteCompany(id);
            return Ok();
        }
        
    }
}
