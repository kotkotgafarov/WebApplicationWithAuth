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

namespace WebApplicationWithAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private IApplicationRepository repo;
        private string userId; // an error can be here. Если этот контроллер общий для всех юзеров. Check is needed

        // constructor injects registered repository 
        public ApplicationController(IApplicationRepository repo,  IHttpContextAccessor context) // UserManager<IdentityUser> userManager,
        {    
            this.userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.repo = repo;
        }


        // GET: api/profile/[id] 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //userId = "319170d1-4433-4334-8e45-64926a2cb29c";
            Application a = await repo.RetrieveApplicationAsync(id, userId);
            return new ObjectResult(a); // 200 OK 
        }

        // PUT: api/enrollees/[id] 
        // BODY: Enrollee (JSON, XML) 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Application a)
        {

            if (a == null || a.enrollee.Id != id)
            {
                return BadRequest(); // 400 Bad request 
            }

            if (ModelState.IsValid)
            {
                await repo.UpdateAsync(id, a);
                return new NoContentResult(); // 204 No content
            }
            return BadRequest(ModelState);
        }

    }
}