using Customs.API.Models;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customs.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Storage> _storageRepository;

    public ProductController(IBaseRepository<Product> productRepository, IBaseRepository<Storage> storageRepository)
    {
        _productRepository = productRepository;
        _storageRepository = storageRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _productRepository.GetEntities()
            .Include(x => x.Storage)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.StorageId,
                StorageName = x.Storage.Name
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("Storages")]
    public async Task<IActionResult> GetStoragesAsync()
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
        await _productRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateProductBody body)
    {
        var entityToUpdate = new Product
        {
            Id = body.Id,
            Name = body.Name,
            StorageId = body.StorageId
        };

        await _productRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateProductBody body)
    {
        var entityToCreate = new Product
        {
            Name = body.Name,
            StorageId = body.StorageId
        };

        await _productRepository.Create(entityToCreate);

        return NoContent();
    }
}