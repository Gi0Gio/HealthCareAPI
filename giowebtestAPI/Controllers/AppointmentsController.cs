using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("patientCreate")]
        public async Task<ActionResult> CreateAppointment(PatientAppointmentDto patientAppointmentDto, int patientId)
        {
            var newAppointment = new Appointment
            {
                PatientId = patientId,
                ClinicId = 1, // Fixed to clinic ID 1 as per requirements
                DoctorId = 2,
                AppointmentDate = patientAppointmentDto.AppointmentDate,
                Type = patientAppointmentDto.Type,
                Description = patientAppointmentDto.Description,
                Status = "Pending"
            };

            _context.Appointments.Add(newAppointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment request submitted successfully!");
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(AppointmentDto appointmentDto)
        {
            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentDate = appointmentDto.AppointmentDate,
                Description = appointmentDto.Description,
                Type = appointmentDto.Type,
                Status = "Pending"
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // PUT: api/Appointments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, AppointmentDto appointmentDto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.PatientId = appointmentDto.PatientId;
            appointment.DoctorId = appointmentDto.DoctorId;
            appointment.AppointmentDate = appointmentDto.AppointmentDate;
            appointment.Description = appointmentDto.Description;
            appointment.Type = appointmentDto.Type;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("clinicUpdate")]
        public async Task<ActionResult> UpdateAppointmentStatus(int appointmentId, int? doctorId, string status)
        {
            // Validate status
            if (status != "Accepted" && status != "Canceled")
            {
                return BadRequest("Invalid status. Only 'Accepted' or 'Canceled' are allowed.");
            }

            // Retrieve the appointment by ID
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            // Update the doctor ID and status if specified
            if (status == "Accepted" && doctorId.HasValue)
            {
                appointment.DoctorId = doctorId.Value; // Assign the doctor
            }
            appointment.Status = status; // Update status to either "Accepted" or "Canceled"

            // Save changes to the database
            await _context.SaveChangesAsync();
            return Ok("Appointment updated successfully.");
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // DELETE: api/Appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
