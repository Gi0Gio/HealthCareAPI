using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
            return await _context.MedicalRecords.ToListAsync();
        }

        // GET: api/MedicalRecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            return medicalRecord;
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecordDto medicalRecordDto)
        {
            var medicalRecord = new MedicalRecord
            {
                PatientId = medicalRecordDto.PatientId,
                DoctorId = medicalRecordDto.DoctorId,
                RecordDate = medicalRecordDto.RecordDate,
                Diagnosis = medicalRecordDto.Diagnosis,
                ProcedurePerformed = medicalRecordDto.ProcedurePerformed,
                Notes = medicalRecordDto.Notes,
                MedicationId = medicalRecordDto.MedicationId
            };

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.Id }, medicalRecord);
        }

        // PUT: api/MedicalRecords/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(int id, MedicalRecordDto medicalRecordDto)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            medicalRecord.PatientId = medicalRecordDto.PatientId;
            medicalRecord.DoctorId = medicalRecordDto.DoctorId;
            medicalRecord.RecordDate = medicalRecordDto.RecordDate;
            medicalRecord.Diagnosis = medicalRecordDto.Diagnosis;
            medicalRecord.ProcedurePerformed = medicalRecordDto.ProcedurePerformed;
            medicalRecord.Notes = medicalRecordDto.Notes;
            medicalRecord.MedicationId = medicalRecordDto.MedicationId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MedicalRecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
