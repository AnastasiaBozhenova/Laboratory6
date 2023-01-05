using System;
using System.Linq;
using System.Linq.Expressions;
using Customs.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Customs.DAL.Repositories
{
    public class DutyRepository : BaseRepository<Duty>
    {
        public DutyRepository(CustomsContext context) : base(context)
        {
        }

        protected override IQueryable<Duty> IncludeChildren(IQueryable<Duty> query)
        {
            return query
                .Include(q => q.Product)
                .Include(q => q.Storage)
                .Include(q => q.Employee);
        }

        protected override Expression<Func<Duty, bool>> GetByIdExpression(int id)
        {
            return duty => duty.Id == id;
        }
    }
}