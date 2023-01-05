using System.Collections.Generic;
using System.Threading.Tasks;
using Customs.DAL.Models;

namespace Customs.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByStorageId(int storageId);
    }
}