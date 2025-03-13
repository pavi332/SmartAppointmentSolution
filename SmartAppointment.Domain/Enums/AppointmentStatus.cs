namespace SmartAppointment.Domain.Enums
{
    public enum AppointmentStatus
    {
        Pending,    // Appointment is booked but not yet confirmed
        Confirmed,  // Appointment is confirmed by the professional
        Canceled    // Appointment is canceled by the user or professional
    }
}
