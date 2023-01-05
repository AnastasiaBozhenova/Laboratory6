using System;
using System.Linq;
using System.Linq.Expressions;
using Customs.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Customs.DAL.Repositories
{
    public class StorageRepository : BaseRepository<Storage>
    {
        public StorageRepository(CustomsContext context) : base(context)
        {
        }

        protected override IQueryable<Storage> IncludeChildren(IQueryable<Storage> query)
        {
            return query
                .Include(q => q.Products)
                .Include(q => q.EmployeeStorages)
                    .ThenInclude(sp => sp.Employee)
                .Include(q => q.Duties);
        }

        protected override Expression<Func<Storage, bool>> GetByIdExpression(int id)
        {
            return storage => storage.Id == id;
        }
    }
}