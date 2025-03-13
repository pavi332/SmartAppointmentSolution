using System.Net.Http.Json;
using SmartAppointment.Web.Models;

namespace SmartAppointment.Web.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppointmentDto>> GetAppointmentsAsync(AppointmentDto appointment)
        {
            return await _httpClient.GetFromJsonAsync<List<AppointmentDto>>("api/appointments");
        }

        public async Task<List<AppointmentDto>> GetUserAppointmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AppointmentDto>>("api/appointments/user");
        }

        public async Task<List<AppointmentDto>> GetProfessionalAppointmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AppointmentDto>>("api/appointments/professional");
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(AppointmentDto appointment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/appointments", appointment);
            return await response.Content.ReadFromJsonAsync<AppointmentDto>();
        }

        public async Task<bool> UpdateAppointmentAsync(AppointmentDto appointment)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/appointments/{appointment.Id}", appointment);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAppointmentAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/appointments/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
