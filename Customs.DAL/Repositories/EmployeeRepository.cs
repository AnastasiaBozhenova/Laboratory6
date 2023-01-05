using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Customs.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Customs.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly CustomsContext _context;

        public EmployeeRepository(CustomsContext context) : base(context)
        {
            _context = context;
        }

        protected override IQueryable<Employee> IncludeChildren(IQueryable<Employee> query)
        {
            return query
                .Include(t => t.EmployeeStorages)
                    .ThenInclude(x => x.Storage)
                .Include(t => t.Duties);
        }

        protected override Expression<Func<Employee, bool>> GetByIdExpression(int id)
        {
            return employee => employee.Id == id;
        }

        public override async Task Create(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public override async Task Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> AutoCompleteEmployees(string request)
        {
            var distributors = await _context.Employees
                .Where(d => d.FirstName.Contains(request) || d.MiddleName.Contains(request) || d.LastName.Contains(request))
                .OrderBy(d => d.LastName)
                .Take(30)
                .ToListAsync();

            return distributors;
        }

        public async Task<List<Employee>> GetEmployeesByStorageId(int storageId)
        {
            var employees = await _context.Employees
                .Include(x => x.EmployeeStorages)
                .Where(p => p.EmployeeStorages.Select(x => x.StorageId).Contains(storageId))
                .ToListAsync();

            return employees;
        }

        private async Task AddOrRemoveEmployeeStorages(Employee employee, List<int> storageIds)
        {
            var employeeStorages = _context.EmployeeStorages
                .Where(x => x.EmployeeId == employee.Id)
                .ToList();

            var existedEmployeeStorage = employeeStorages
                .Where(x => storageIds.Contains(x.StorageId))
                .ToList();

            var entitiesForDelete = employeeStorages
                .Except(existedEmployeeStorage);

            _context.EmployeeStorages.RemoveRange(entitiesForDelete);

            var entitiesForAdd = storageIds
                .Except(employeeStorages.Select(x => x.StorageId));

            _context.EmployeeStorages.AddRange(entitiesForAdd.Select(s => new EmployeeStorage
            {
                EmployeeId = employee.Id,
                StorageId = s
            }));

            await _context.SaveChangesAsync();
        }
    }
}