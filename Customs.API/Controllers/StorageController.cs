using Customs.API.Models;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customs.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{
    private readonly IBaseRepository<Storage> _storageRepository;

    public StorageController(IBaseRepository<Storage> storageRepository)
    {
        _storageRepository = storageRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _storageRepository.GetEntities()
            .Select(x => new
            {
                x.Id,
                x.Name
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _storageRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateStorageBody body)
    {
        var entityToUpdate = new Storage
        {
            Id = body.Id,
            Name = body.Name
        };

        await _storageRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateStorageBody body)
    {
        var entityToCreate = new Storage
        {
            Name = body.Name
        };

        await _storageRepository.Create(entityToCreate);

        return NoContent();
    }
}