using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApplicationWithAuth.Data;
using Microsoft.AspNetCore.Identity;

namespace WebApplicationWithAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private string userId; // an error can be here. Если этот контроллер общий для всех юзеров. Check is needed
        private ApplicationDbContext db;

        // constructor injects registered repository 
        public UsersController(ApplicationDbContext db,  IHttpContextAccessor context) // UserManager<IdentityUser> userManager,
        {    

            this.userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.db = db;
        }

        // GET: api/users 
        [HttpGet]
        public async Task<IEnumerable<IdentityUser>> Get()
        {
            return await Task.Run<IEnumerable<IdentityUser>>(
                () => db.Users.ToList());
        }

        // GET: api/users/[id] 
        [HttpGet("{id}")]
        public async Task<LKAUser> Get(int id)
        {
            //this.userId = "319170d1-4433-4334-8e45-64926a2cb29c";
            LKAUser lkaUser = new LKAUser() { isModerator = false };

            IdentityUser _user = db.Users.First(r => r.Id == userId);
            if (_user !=null)
            {
                lkaUser.Name = _user.UserName;
                
                IdentityUserRole<string> role = db.UserRoles.First(r => r.UserId == userId && r.RoleId == "2");
                if (role != null)
                {
                    lkaUser.isModerator = true;
                }
            }
            return lkaUser;
        }
    }
}