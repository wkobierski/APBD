using System.Text.Encodings.Web;
using System.Text.Unicode;
using cwiczenia6.Data;
using cwiczenia6.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddDbContext<HospitalContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "Cwiczenia 6");
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
