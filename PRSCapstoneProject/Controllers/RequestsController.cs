using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using PRSCapstoneProject.Models;

namespace PRSCapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSDbContext _context;

        public RequestsController(PRSDbContext context)
        {
            _context = context;
        }


        //Skyler, the only issue is not getting the virtual instances when
        //reading for all the requests or by by id.You should be including the
        //USER in both GET methods and you should also include the Requestlines in
        //the get by id along with the Products for the Requestline.


        //Getting requests in review status that are not from the user: api/requests/reviews/{userId}
        [HttpGet("reviews/{userId}")]
        public async Task<List<Request>> GetReviews(int userId)
        {
            var request = await _context.Requests
                .Where(r => r.Status == "REVIEW" && r.UserId != userId)
                .ToListAsync();
                
            return request;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests
                .Include(r => r.User)
                .ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.Include(x => x.User)
                                                 .Include(x => x.RequestLines)
                                                 .ThenInclude(x => x.Product)
                                                 .SingleOrDefaultAsync(i=>i.Id==id);
            
            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Setting the status of the request
        [HttpPut("review/{id}")]
        public async Task<IActionResult> Review(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            if (request.Total <= 50)
            {
                request.Status = "APPROVED";
            }
            else
            {
                request.Status = "REVIEW";
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("approve/{id}")]
        public async Task Approve(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                NotFound();
            }
            else
            {
                request.Status = "APPROVED";
            }
            await _context.SaveChangesAsync();
            Ok();
        }
        [HttpPut("reject/{id}")]
        public async Task Reject(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                NotFound();
            }
            else
            {
                request.Status = "REJECTED";
            }
            await _context.SaveChangesAsync();
            Ok();
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
