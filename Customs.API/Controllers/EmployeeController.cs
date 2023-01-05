using Customs.API.Models;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customs.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IBaseRepository<Employee> _employeeRepository;

    public EmployeeController(IBaseRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _employeeRepository.GetEntities()
            .Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.MiddleName,
                x.Role
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _employeeRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateEmployeeBody body)
    {
        var entityToUpdate = new Employee
        {
            Id = body.Id,
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName,
            Role = body.Role,
            IdNumber = ""
        };

        await _employeeRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateEmployeeBody body)
    {
        var entityToCreate = new Employee
        {
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName,
            Role = body.Role,
            IdNumber = ""
        };

        await _employeeRepository.Create(entityToCreate);

        return NoContent();
    }
}