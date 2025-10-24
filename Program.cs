using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddScoped<RepositorioMascota>();
builder.Services.AddScoped<RepositorioDueno>();
builder.Services.AddScoped<RepositorioVeterinario>();
builder.Services.AddScoped<RepositorioEspecialidad>();
builder.Services.AddScoped<RepositorioServicioMedico>();
builder.Services.AddScoped<RepositorioRegistroClinico>();
builder.Services.AddScoped<RepositorioVacuna>();
builder.Services.AddScoped<RepositorioCita>();


builder.Services.AddScoped<MascotaService>();
builder.Services.AddScoped<DuenoService>();
builder.Services.AddScoped<VeterinarioService>();
builder.Services.AddScoped<EspecialidadService>();
builder.Services.AddScoped<ServicioMedicoService>();
builder.Services.AddScoped<RegistroClinicoService>();
builder.Services.AddScoped<VacunaService>();
builder.Services.AddScoped<CitaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Veterinaria API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Veterinaria API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();