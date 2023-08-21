using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MeetingsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Meetings
        [HttpGet]
        public async Task<ActionResult> GetMeetings()
        {
            var meetings = await _context.Meetings.ToListAsync();
            return Ok(meetings);
        }

        // GET: api/Meetings/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMeeting(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }

        // POST: api/Meetings
        [HttpPost]
        public async Task<ActionResult> CreateMeeting(Meeting meeting)
        {
            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeeting), new { id = meeting.Id }, meeting);
        }

        // PUT: api/Meetings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeeting(int id, Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return BadRequest();
            }

            _context.Entry(meeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(id))
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

        // DELETE: api/Meetings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.Id == id);
        }
    }
}
