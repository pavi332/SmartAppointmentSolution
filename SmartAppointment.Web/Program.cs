using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using SmartAppointment.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ✅ Add Authentication & Authorization
builder.Services.AddAuthenticationCore();

// ✅ Register HttpClient globally (Recommended approach)
builder.Services.AddHttpClient("BaseUrl", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiEndpoints:BaseUrl"]);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });
// ✅ Register Services with Dependency Injection
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<AuthService>();

// ✅ Register Authentication State Provider
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

var app = builder.Build();

// ✅ Configure the HTTP Request Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ✅ Ensure authentication & authorization are applied
app.UseAuthentication();
app.UseAuthorization();

// ✅ Map Blazor and fallback routes
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
