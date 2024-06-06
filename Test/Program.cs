using Test.Interfaces;
using Test.Services;

var builder = WebApplication.CreateBuilder(args);

//License.LicenseKey = "IRONSUITE.ALEXGUILLEN265.GMAIL.COM.14721-ADB551F70D-BWK6EWLTPIPEZVCF-K65BN65FAPZN-PVDMN4BDNN7R-JHDFPJSM443J-YLRYTA4FTCKI-HRBG6JBXWWOC-QWH2ZP-TUGE6UXAHXOMUA-DEPLOYMENT.TRIAL-DPSLGY.TRIAL.EXPIRES.04.JUL.2024";
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
// Add services to the container.

builder.Services.AddScoped<IClientesService, ClienteServices>();
builder.Services.AddScoped<IGeneratePdfService, GeneratePdfService>();
builder.Services.AddScoped<IReportesDiversosService, ReportesDiversosService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
