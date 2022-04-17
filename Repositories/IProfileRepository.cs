using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationWithAuth.Models;

namespace WebApplicationWithAuth
{
    public interface IProfileRepository
    {
        Task<Profile> RetrieveProfileAsync(int id, string UserId);

        Task<Profile> UpdateAsync(int id, Profile c);
    }
}