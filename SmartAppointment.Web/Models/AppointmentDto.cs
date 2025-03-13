using System;

namespace SmartAppointment.Web.Models
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }  // For booking user's ID
        public Guid ProfessionalId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }  // e.g., "Pending", "Confirmed", "Canceled"
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
