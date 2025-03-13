using SmartAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAppointment.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<List<Appointment>> GetAppointmentsAsync();
        Task<List<Appointment>> GetAppointmentsByUserIdAsync(string userId);
        Task<List<Appointment>> GetAppointmentsByProfessionalIdAsync(string professionalId); // ✅ FIX: Add this method
        Task<Appointment> GetAppointmentByIdAsync(Guid id);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(Guid id);
    }
}
