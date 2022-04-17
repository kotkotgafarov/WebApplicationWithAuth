using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationWithAuth.Models;
using WebApplicationWithAuth.Common;

namespace WebApplicationWithAuth
{
    public interface IEnrolleeRepository
    {
        Task<Enrollee> CreateAsync(Enrollee c);

        Task<PageResult<Enrollee>> RetrieveAllAsync(GetEnrolleesQueryObject request);

        Task<Enrollee> RetrieveAsync(int id);

        Task<Enrollee> RetrieveProfileAsync(string UserId);

        Task<Enrollee> UpdateAsync(int id, Enrollee c);

        Task<bool> DeleteAsync(int id);
    }
}