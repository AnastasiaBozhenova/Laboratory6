using System.Collections.Generic;
using System.Threading.Tasks;
using Customs.DAL.Models;

namespace Customs.DAL.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> AutoCompleteEmployees(string request);
        Task<List<Employee>> GetEmployeesByStorageId(int storageId);
    }
}