using System;

namespace SmartAppointment.Domain.Entities
{
    public class ProfessionalAvailability
    {
        public Guid Id { get;  set; } = Guid.NewGuid();

        // Foreign key: the professional this availability belongs to
        public Guid ProfessionalId { get; set; }

        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Constructor used when creating a new availability slot
        public ProfessionalAvailability(Guid professionalId, DateTime availableDate, TimeSpan startTime, TimeSpan endTime)
        {
            ProfessionalId = professionalId;
            AvailableDate = availableDate;
            StartTime = startTime;
            EndTime = endTime;
            IsBooked = false;
        }

        // Parameterless constructor for EF Core
        protected ProfessionalAvailability() { }

        // Method to mark the slot as booked
        public void MarkAsBooked()
        {
            IsBooked = true;
        }
    }
}
