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
using WebApplicationWithAuth.Common;

namespace WebApplicationWithAuth.Controllers
{
    // base address: api/enrollees 
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolleesController : Controller
    {
        private IEnrolleeRepository repo;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly IHttpContextAccessor _context;
        private string userId; // an error can be here. Если этот контроллер общий для всех юзеров. Check is needed

        // constructor injects registered repository 
        public EnrolleesController(IEnrolleeRepository repo,  IHttpContextAccessor context) // UserManager<IdentityUser> userManager,
        {    
            //_userManager = userManager;
            //_context = context;
            this.userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user =  _userManager.FindByIdAsync(userId);
            this.repo = repo;
        }

        // GET: api/enrollees 
        // GET: api/enrollees/?country=[country] 
        [HttpGet]
        public async Task<PageResult<Enrollee>> Get([FromQuery] GetEnrolleesQueryObject request)
        {
            return await repo.RetrieveAllAsync(request);
        }

        // GET: api/enrollees/[id] 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Enrollee c;
            if (id==0)
            {
                userId = "319170d1-4433-4334-8e45-64926a2cb29c";
                c = await repo.RetrieveProfileAsync(userId);
                if (c == null)
                {
                    c = new Enrollee { UserID = userId };
                    await repo.CreateAsync(c);
                }

            }
            else
            {
                c = await repo.RetrieveAsync(id);
                if (c == null)
                {
                    return NotFound(); // 404 Resource not found 
                }
            }
 
            return new ObjectResult(c); // 200 OK 
        }


        // POST: api/enrollees 
        // BODY: Enrollee (JSON, XML) 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Enrollee c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request 
            }

            c.UserID = "111";
            await repo.CreateAsync(c);

            return CreatedAtRoute("Get", // use named route
                new { id = c.Id }, c); // 201 Created 
        }

        // PUT: api/enrollees/[id] 
        // BODY: Enrollee (JSON, XML) 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Enrollee c)
        {

            if (c == null || c.Id != id)
            {
                return BadRequest(); // 400 Bad request 
            }

            if (ModelState.IsValid)
            {
                await repo.UpdateAsync(id, c);
                return new NoContentResult(); // 204 No content
            }
            return BadRequest(ModelState);
            /*var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }*/
        }

        // DELETE: api/enrollees/[id] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await repo.RetrieveAsync(id);

            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            bool deleted = await repo.DeleteAsync(id);

            if (deleted)
            {
                return new NoContentResult(); // 204 No content 
            }
            else
            {
                return BadRequest();
            }
        }
    }
}