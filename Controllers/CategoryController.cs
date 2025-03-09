using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Context;
using Survivor.Entities;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CategoryController(SurvivorDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryEntity>>> GetCategories()
        {
            return await _context.Categories
                                 .Where(c => !c.IsDeleted) // Silinmemiş kayıtları getir
                                 .ToListAsync();
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryEntity>> GetCategory(int id)
        {
            var category = await _context.Categories
                                         .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategory(CategoryEntity category)
        {
            // Competitors alanını boş bir koleksiyonla başlat
            category.Competitors = new List<CompetitorsEntity>();

            category.CreatedDate = DateTime.Now;
            category.ModifiedDate = DateTime.Now;
            category.IsDeleted = false;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryEntity category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var existingCategory = await _context.Categories
                                                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = category.Name;
            existingCategory.ModifiedDate = DateTime.Now;

            _context.Entry(existingCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                                         .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            category.IsDeleted = true; // Soft delete
            category.ModifiedDate = DateTime.Now;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id && !e.IsDeleted);
        }
    }
}