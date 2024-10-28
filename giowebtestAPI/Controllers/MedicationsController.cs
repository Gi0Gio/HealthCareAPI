using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medication>>> GetMedications()
        {
            return await _context.Medications.ToListAsync();
        }

        // GET: api/Medications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Medication>> GetMedication(int id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            return medication;
        }

        // POST: api/Medications
        [HttpPost]
        public async Task<ActionResult<Medication>> PostMedication(MedicationDto medicationDto)
        {
            var medication = new Medication
            {
                Name = medicationDto.Name,
                Description = medicationDto.Description,
                Quantity = medicationDto.Quantity,
                ExpirationDate = medicationDto.ExpirationDate,
                RecommendedDosage = medicationDto.RecommendedDosage
            };

            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedication), new { id = medication.Id }, medication);
        }

        // PUT: api/Medications/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication(int id, MedicationDto medicationDto)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            medication.Name = medicationDto.Name;
            medication.Description = medicationDto.Description;
            medication.Quantity = medicationDto.Quantity;
            medication.ExpirationDate = medicationDto.ExpirationDate;
            medication.RecommendedDosage = medicationDto.RecommendedDosage;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Medications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
