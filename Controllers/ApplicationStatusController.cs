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
    // base address: api/applicationstatus 
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationStatusController : Controller
    {

        private string userId; // an error can be here. Если этот контроллер общий для всех юзеров. Check is needed
        private LKADbContext db;

        // constructor injects registered repository 
        public ApplicationStatusController(LKADbContext db, IHttpContextAccessor context)
        {

            this.userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.db = db;
        }


        // PUT: api/applicationstatus/[id] 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EnrolleesStatus e)
        {
            if (e.EnrolleeId != id)
            {
                return BadRequest();
            }

            EnrolleesStatus appStatus = db.EnrolleesStatuses.First(s => s.EnrolleeId == id);
            appStatus.ProfileStatus = e.ProfileStatus;
            appStatus.ApplicationStatus = e.ApplicationStatus;
            db.EnrolleesStatuses.Update(appStatus);

            int affected = db.SaveChanges();

            if (affected > 0)
            {
                 return Ok();
            }
            return new NoContentResult(); // 204 No content
        }
    }
}