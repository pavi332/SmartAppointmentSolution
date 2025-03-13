using Microsoft.EntityFrameworkCore;
using SmartAppointment.Application.Interfaces;
using SmartAppointment.Domain.Entities;
using SmartAppointment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAppointment.Infrastructure.Persistence
{
    public class AppointmentRepository : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Implement GetAppointmentsAsync()
        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        // ✅ Implement GetAllAppointmentsAsync() (this was missing)
        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();  // Retrieves all appointments
        }

        public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(string userId)
        {
            return await _context.Appointments
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByProfessionalIdAsync(string professionalId)
        {
            return await _context.Appointments
                .Where(a => a.ProfessionalId.ToString() == professionalId)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
