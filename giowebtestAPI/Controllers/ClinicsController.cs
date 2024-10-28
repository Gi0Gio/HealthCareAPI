using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClinicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics()
        {
            return await _context.Clinics.ToListAsync();
        }

        // GET: api/Clinics/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinic>> GetClinic(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            return clinic;
        }

        // POST: api/Clinics
        [HttpPost]
        public async Task<ActionResult<Clinic>> PostClinic(ClinicDto clinicDto)
        {
            var clinic = new Clinic
            {
                Name = clinicDto.Name,
                Address = clinicDto.Address,
                PhoneNumber = clinicDto.PhoneNumber,
                Email = clinicDto.Email,
                OfficeHours = clinicDto.OfficeHours
            };

            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClinic), new { id = clinic.Id }, clinic);
        }

        // PUT: api/Clinics/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinic(int id, ClinicDto clinicDto)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            clinic.Name = clinicDto.Name;
            clinic.Address = clinicDto.Address;
            clinic.PhoneNumber = clinicDto.PhoneNumber;
            clinic.Email = clinicDto.Email;
            clinic.OfficeHours = clinicDto.OfficeHours;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Clinics/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
