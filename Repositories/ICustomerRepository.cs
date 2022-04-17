using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationWithAuth.Models;

namespace WebApplicationWithAuth
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer c);

        Task<IEnumerable<Customer>> RetrieveAllAsync();

        Task<Customer> RetrieveAsync(string id);

        Task<Customer> UpdateAsync(string id, Customer c);

        Task<bool> DeleteAsync(string id);
    }
}