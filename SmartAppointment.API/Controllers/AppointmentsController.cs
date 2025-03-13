using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAppointment.Application.Interfaces;
using SmartAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires Authentication (JWT)
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // ✅ 1️⃣ GET: api/appointments (Only Admins can view all appointments)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            return Ok(await _appointmentService.GetAllAppointmentsAsync());
        }

        // ✅ 2️⃣ GET: api/appointments/user (Users can view their own appointments)
        [Authorize(Roles = "User")]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var appointments = await _appointmentService.GetAppointmentsByUserIdAsync(userId);
            return Ok(appointments);
        }

        // ✅ 3️⃣ GET: api/appointments/professional (Professionals can view their own appointments)
        [Authorize(Roles = "Professional")]
        [HttpGet("professional")]
        public async Task<IActionResult> GetProfessionalAppointments()
        {
            var professionalId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (professionalId == null) return Unauthorized();

            var appointments = await _appointmentService.GetAppointmentsByProfessionalIdAsync(professionalId);
            return Ok(appointments);
        }

        // ✅ 4️⃣ GET: api/appointments/{id} (View Appointment by ID)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        // ✅ 5️⃣ POST: api/appointments (Users can book an appointment)
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            appointment.AssignUser(userId); // ✅ Fix: Use method instead of direct property setting

            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.Id }, createdAppointment);
        }

        // ✅ 6️⃣ PUT: api/appointments/{id} (Users can modify their appointments)
        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] Appointment updatedAppointment)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null || appointment.UserId != userId) return NotFound();

            updatedAppointment.Id = id;
            var success = await _appointmentService.UpdateAppointmentAsync(updatedAppointment);
            return success ? NoContent() : StatusCode(500, "Error updating appointment");
        }

        // ✅ 7️⃣ DELETE: api/appointments/{id} (Users can cancel their appointments)
        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null || appointment.UserId != userId) return NotFound();

            var success = await _appointmentService.DeleteAppointmentAsync(id);
            return success ? NoContent() : StatusCode(500, "Error deleting appointment");
        }

        // ✅ 8️⃣ POST: api/appointments/confirm/{id} (Professionals can confirm appointments)
        [Authorize(Roles = "Professional")]
        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> ConfirmAppointment(Guid id)
        {
            var professionalId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (professionalId == null) return Unauthorized();

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null || appointment.ProfessionalId.ToString() != professionalId) return NotFound();

            appointment.Confirm();
            var success = await _appointmentService.UpdateAppointmentAsync(appointment);
            return success ? Ok(appointment) : StatusCode(500, "Error confirming appointment");
        }
    }
}
