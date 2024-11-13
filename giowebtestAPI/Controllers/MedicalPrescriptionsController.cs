using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalPrescriptionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicalPrescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicalPrescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalPrescription>>> GetMedicalPrescriptions()
        {
            return await _context.MedicalPrescriptions.ToListAsync();
        }

        // GET: api/MedicalPrescriptions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalPrescription>> GetMedicalPrescription(int id)
        {
            var medicalPrescription = await _context.MedicalPrescriptions.FindAsync(id);
            if (medicalPrescription == null)
            {
                return NotFound();
            }

            return medicalPrescription;
        }

        // GET: api/MedicalPrescriptions/ByDoctor/{doctorId}
        [HttpGet("ByDoctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<MedicalPrescription>>> GetMedicalPrescriptionsByDoctor(int doctorId)
        {
            var prescriptions = await _context.MedicalPrescriptions
                                              .Where(mp => mp.DoctorId == doctorId)
                                              .ToListAsync();
            if (!prescriptions.Any())
            {
                return NotFound($"No se encontraron prescripciones para el Doctor con ID: {doctorId}");
            }

            return prescriptions;
        }

        // GET: api/MedicalPrescriptions/ByPatient/{patientId}
        [HttpGet("ByPatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalPrescription>>> GetMedicalPrescriptionsByPatient(int patientId)
        {
            var prescriptions = await _context.MedicalPrescriptions
                                              .Where(mp => mp.PatientId == patientId)
                                              .ToListAsync();
            if (!prescriptions.Any())
            {
                return NotFound($"No se encontraron prescripciones para el Paciente con ID: {patientId}");
            }

            return prescriptions;
        }

        // POST: api/MedicalPrescriptions
        [HttpPost]
        public async Task<ActionResult<MedicalPrescription>> PostMedicalPrescription(MedicalPrescriptionDto medicalPrescriptionDto)
        {
            var medicalPrescription = new MedicalPrescription
            {
                PatientId = medicalPrescriptionDto.PatientId,
                DoctorId = medicalPrescriptionDto.DoctorId,
                IssueDate = medicalPrescriptionDto.IssueDate,
                Description = medicalPrescriptionDto.Description,
                Dosage = medicalPrescriptionDto.Dosage,
                MedicationId = medicalPrescriptionDto.MedicationId
            };

            _context.MedicalPrescriptions.Add(medicalPrescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalPrescription), new { id = medicalPrescription.Id }, medicalPrescription);
        }

        // PUT: api/MedicalPrescriptions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalPrescription(int id, MedicalPrescriptionDto medicalPrescriptionDto)
        {
            var medicalPrescription = await _context.MedicalPrescriptions.FindAsync(id);
            if (medicalPrescription == null)
            {
                return NotFound();
            }

            medicalPrescription.PatientId = medicalPrescriptionDto.PatientId;
            medicalPrescription.DoctorId = medicalPrescriptionDto.DoctorId;
            medicalPrescription.IssueDate = medicalPrescriptionDto.IssueDate;
            medicalPrescription.Description = medicalPrescriptionDto.Description;
            medicalPrescription.Dosage = medicalPrescriptionDto.Dosage;
            medicalPrescription.MedicationId = medicalPrescriptionDto.MedicationId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MedicalPrescriptions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalPrescription(int id)
        {
            var medicalPrescription = await _context.MedicalPrescriptions.FindAsync(id);
            if (medicalPrescription == null)
            {
                return NotFound();
            }

            _context.MedicalPrescriptions.Remove(medicalPrescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
