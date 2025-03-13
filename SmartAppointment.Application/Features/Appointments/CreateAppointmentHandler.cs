using SmartAppointment.Application.Interfaces;
using SmartAppointment.Domain.Entities;

namespace SmartAppointment.Application.Features.Appointments
{
    public class CreateAppointmentHandler
    {
        private readonly IAppointmentService _appointmentService;

        public CreateAppointmentHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Appointment> Handle(Appointment appointment)
        {
            return await _appointmentService.CreateAppointmentAsync(appointment);
        }
    }
}
