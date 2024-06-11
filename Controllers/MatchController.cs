using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sgbet.Db;
using sgbet.Dtos;
using sgbet.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatches()
        {
            return await _context.Matches
                .Select(m => new MatchDto
                {
                    Id = m.Id,
                    HostId = m.HostId,
                    AwayId = m.AwayId,
                    DateAndTime = m.DateAndTime,
                    IsEnded = m.IsEnded,
                    ResultHome = m.ResultHome,
                    ResultAway = m.ResultAway
                }).ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return new MatchDto
            {
                Id = match.Id,
                HostId = match.HostId,
                AwayId = match.AwayId,
                DateAndTime = match.DateAndTime,
                IsEnded = match.IsEnded,
                ResultHome = match.ResultHome,
                ResultAway = match.ResultAway
            };
        }

        // POST: api/Matches
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(CreateMatchDto createMatchDto)
        {
            if (createMatchDto.DateAndTime < DateTime.Now)
            {
                return Conflict("Date must be bigger than now");
            }
            var match = new Match
            {
                HostId = createMatchDto.HostId,
                AwayId = createMatchDto.AwayId,
                DateAndTime = createMatchDto.DateAndTime,
                IsEnded = false,
                ResultHome = null,
                ResultAway = null
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, new MatchDto
            {
                Id = match.Id,
                HostId = match.HostId,
                AwayId = match.AwayId,
                DateAndTime = match.DateAndTime,
                IsEnded = match.IsEnded,
                ResultHome = match.ResultHome,
                ResultAway = match.ResultAway
            });
        }

        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, MatchDto matchDto)
        {
            if (id != matchDto.Id)
            {
                return BadRequest();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            match.HostId = matchDto.HostId;
            match.AwayId = matchDto.AwayId;
            match.DateAndTime = matchDto.DateAndTime;
            match.IsEnded = matchDto.IsEnded;
            match.ResultHome = matchDto.ResultHome;
            match.ResultAway = matchDto.ResultAway;

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }

        [HttpPatch("EndMatch/{id}")]
        public async Task<IActionResult> EndMatch(int id, [FromBody] EndMatchDto endMatchDto)
        {
            if (id != endMatchDto.Id)
            {
                return BadRequest("ID in the request body doesn't match the ID in the URL.");
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            match.ResultHome = endMatchDto.HomeScore;
            match.ResultAway = endMatchDto.AwayScore;
            match.IsEnded = true;

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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
    }


}