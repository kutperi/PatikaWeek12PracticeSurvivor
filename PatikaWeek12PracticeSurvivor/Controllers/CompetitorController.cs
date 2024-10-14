using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaWeek12PracticeSurvivor.Context;
using PatikaWeek12PracticeSurvivor.Entities;

namespace PatikaWeek12PracticeSurvivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CompetitorController(SurvivorDbContext context)
        {
            _context = context;
        }

        // GET /api/competitors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var competitors = await _context.Competitors.Include(c => c.Category)
                                                        .ToListAsync();

            return Ok(competitors);
        }

        // GET /api/competitors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompetitor(int id)
        {
            var competitor = await _context.Competitors.Include(c => c.Category)
                                                       .FirstOrDefaultAsync(c => c.Id == id);
            
            if (competitor is null)
                return NotFound();

            return Ok(competitor);
        }

        // GET /api/competitors/categories/{categoryid}
        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetCompetitorsByCategoryId(int categoryId)
        {
            var competitors = await _context.Competitors.Where(c => c.CategoryId == categoryId)
                                                        .Include(c => c.Category)
                                                        .ToListAsync();

            return Ok(competitors);
        }

        // POST /api/competitors
        [HttpPost]
        public async Task<IActionResult> CreateCompetitor([FromBody] CompetitorEntity competitor)
        {
            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetCompetitor), new { id = competitor.Id }, competitor);
        }

        // PUT /api/competitors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompetitor(int id, [FromBody] CompetitorEntity competitor)
        {
            if (id != competitor.Id)
                return BadRequest();

            _context.Entry(competitor).State = EntityState.Modified;
            _context.Competitors.Update(competitor);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE /api/competitors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);

            if (competitor is null)
                return NotFound();

            _context.Competitors.Remove(competitor);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
