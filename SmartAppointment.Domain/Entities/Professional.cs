using System;

namespace SmartAppointment.Domain.Entities
{
    public class Professional
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Reference to the IdentityUser (stored as string key) for authentication details
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Constructor used when creating a new professional
        public Professional(string name, string specialization, string email, string phoneNumber, string userId)
        {
            Name = name;
            Specialization = specialization;
            Email = email;
            PhoneNumber = phoneNumber;
            UserId = userId;
        }

        // Parameterless constructor for EF Core
        protected Professional() { }
    }
}
