using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Context;
using Survivor.Entities;

namespace Survivor.Controllers
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

        // GET: api/competitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitorsEntity>>> GetCompetitors()
        {
            return await _context.Competitors
                                  .Where(c => !c.IsDeleted) // Silinmemiş kayıtları getir
                                  .ToListAsync();
        }

        // GET: api/competitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitorsEntity>> GetCompetitor(int id)
        {
            var competitor = await _context.Competitors
                                           .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (competitor == null)
            {
                return NotFound();
            }

            return competitor;
        }

        // GET: api/competitors/categories/5
        [HttpGet("categories/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CompetitorsEntity>>> GetCompetitorsByCategory(int categoryId)
        {
            var competitors = await _context.Competitors
                                            .Where(c => c.CategoryId == categoryId && !c.IsDeleted)
                                            .ToListAsync();

            if (!competitors.Any())
            {
                return NotFound();
            }

            return competitors;
        }

        // POST: api/competitors
        [HttpPost]
        public async Task<ActionResult<CompetitorsEntity>> PostCompetitor(CompetitorsEntity competitor)
        {
            // Category alanını null yapın (isteğe gönderilmemeli)
            competitor.Category = null;

            competitor.CreatedDate = DateTime.Now;
            competitor.ModifiedDate = DateTime.Now;
            competitor.IsDeleted = false;

            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetitor", new { id = competitor.Id }, competitor);
        }

        // PUT: api/competitors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetitor(int id, CompetitorsEntity competitor)
        {
            // Güncellenecek yarışmacıyı bul
            var existingCompetitor = await _context.Competitors
                                                  .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (existingCompetitor == null)
            {
                return NotFound(); // Yarışmacı bulunamazsa 404 döndür
            }

            // Sadece güncellenmesi gereken alanları güncelle
            existingCompetitor.FirstName = competitor.FirstName;
            existingCompetitor.LastName = competitor.LastName;
            existingCompetitor.CategoryId = competitor.CategoryId;
            existingCompetitor.ModifiedDate = DateTime.Now;

            _context.Entry(existingCompetitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Değişiklikleri kaydet
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitorExists(id))
                {
                    return NotFound(); // Yarışmacı bulunamazsa 404 döndür
                }
                else
                {
                    throw; // Diğer hataları fırlat
                }
            }

            return NoContent(); // Başarılı güncelleme durumunda 204 döndür
        }
        // DELETE: api/competitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors
                                           .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (competitor == null)
            {
                return NotFound();
            }

            competitor.IsDeleted = true; // Soft delete
            competitor.ModifiedDate = DateTime.Now;

            _context.Entry(competitor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompetitorExists(int id)
        {
            return _context.Competitors.Any(e => e.Id == id && !e.IsDeleted);
        }
    }
}