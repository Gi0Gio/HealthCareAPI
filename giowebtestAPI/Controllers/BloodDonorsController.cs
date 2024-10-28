using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodDonorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloodDonorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BloodDonors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodDonor>>> GetBloodDonors()
        {
            return await _context.BloodDonors.ToListAsync();
        }

        // GET: api/BloodDonors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodDonor>> GetBloodDonor(int id)
        {
            var bloodDonor = await _context.BloodDonors.FindAsync(id);
            if (bloodDonor == null)
            {
                return NotFound();
            }

            return bloodDonor;
        }

        // POST: api/BloodDonors
        [HttpPost]
        public async Task<ActionResult<BloodDonor>> PostBloodDonor(BloodDonorDto bloodDonorDto)
        {
            var bloodDonor = new BloodDonor
            {
                Name = bloodDonorDto.Name,
                Identification = bloodDonorDto.Identification,
                BloodType = bloodDonorDto.BloodType,
                LastDonationDate = bloodDonorDto.LastDonationDate
            };

            _context.BloodDonors.Add(bloodDonor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBloodDonor), new { id = bloodDonor.Id }, bloodDonor);
        }

        // PUT: api/BloodDonors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodDonor(int id, BloodDonorDto bloodDonorDto)
        {
            var bloodDonor = await _context.BloodDonors.FindAsync(id);
            if (bloodDonor == null)
            {
                return NotFound();
            }

            bloodDonor.Name = bloodDonorDto.Name;
            bloodDonor.Identification = bloodDonorDto.Identification;
            bloodDonor.BloodType = bloodDonorDto.BloodType;
            bloodDonor.LastDonationDate = bloodDonorDto.LastDonationDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/BloodDonors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodDonor(int id)
        {
            var bloodDonor = await _context.BloodDonors.FindAsync(id);
            if (bloodDonor == null)
            {
                return NotFound();
            }

            _context.BloodDonors.Remove(bloodDonor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
