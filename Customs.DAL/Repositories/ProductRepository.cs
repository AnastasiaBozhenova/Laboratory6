using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Customs.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Customs.DAL.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly CustomsContext _context;

        public ProductRepository(CustomsContext context) : base(context)
        {
            _context = context;
        }

        protected override IQueryable<Product> IncludeChildren(IQueryable<Product> query)
        {
            return query
                .Include(q => q.Storage)
                .Include(q => q.Duty);
        }

        protected override Expression<Func<Product, bool>> GetByIdExpression(int id)
        {
            return product => product.Id == id;
        }

        public async Task<List<Product>> GetProductsByStorageId(int storageId)
        {
            var products = await _context.Products
                .Where(p => p.StorageId == storageId)
                .ToListAsync();

            return products;
        }
    }
}