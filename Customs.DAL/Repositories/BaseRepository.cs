using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Customs.DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly CustomsContext _context;

        protected BaseRepository(CustomsContext context)
        {
            _context = context;
        }

        public virtual async Task Create(TEntity entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetEntityById(id);

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetEntities()
        {
            var query = IncludeChildren(_context.Set<TEntity>())
                .AsNoTracking();

            return query;
        }

        public async Task<TEntity> GetEntityById(int entityId)
        {
            var item = await GetEntities()
                .FirstOrDefaultAsync(GetByIdExpression(entityId));

            return item;
        }

        protected abstract IQueryable<TEntity> IncludeChildren(IQueryable<TEntity> query);

        protected abstract Expression<Func<TEntity, bool>> GetByIdExpression(int id);
    }
}