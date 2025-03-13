using SmartAppointment.Application.Interfaces;
using SmartAppointment.Domain.Entities;

namespace SmartAppointment.Application.Features.Appointments
{
    public class GetAppointmentsHandler
    {
        private readonly IAppointmentService _appointmentService;

        public GetAppointmentsHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<List<Appointment>> Handle()
        {
            return await _appointmentService.GetAppointmentsAsync();
        }
    }
}
