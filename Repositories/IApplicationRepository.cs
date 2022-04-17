using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationWithAuth.Models;

namespace WebApplicationWithAuth
{
    public interface IApplicationRepository
    {
        Task<Application> RetrieveApplicationAsync(int id, string UserId);

        Task<Application> UpdateAsync(int id, Application a);
    }
}