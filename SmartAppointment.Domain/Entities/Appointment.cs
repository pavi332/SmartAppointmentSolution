using System;
using SmartAppointment.Domain.Enums;

namespace SmartAppointment.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // The user (client) who booked the appointment
        public string UserId { get; set; }

        // The professional assigned to this appointment
        public Guid ProfessionalId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property (EF Core Relationship)
        public Professional Professional { get; set; }

        // ✅ Public Constructor (Fix for inaccessible constructor issue)
        public Appointment() { }

        // Constructor used when creating a new appointment
        public Appointment(string userId, Guid professionalId, DateTime appointmentDate)
        {
            UserId = userId;
            ProfessionalId = professionalId;
            AppointmentDate = appointmentDate;
            Status = AppointmentStatus.Pending; // Default status
        }

        // ✅ Optional Factory Method
        public static Appointment Create(string userId, Guid professionalId, DateTime appointmentDate)
        {
            return new Appointment(userId, professionalId, appointmentDate);
        }

        // ✅ Method to set UserId (Fix for inaccessible setter issue)
        public void AssignUser(string userId)
        {
            if (string.IsNullOrEmpty(UserId)) // Prevent overwriting
            {
                UserId = userId;
            }
        }

        // Methods to update the appointment status
        public void Confirm()
        {
            Status = AppointmentStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = AppointmentStatus.Canceled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
