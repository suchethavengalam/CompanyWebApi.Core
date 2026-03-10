using Companies.Core.Business.Interfaces;
using Companies.Core.Business.Services;
using Companies.Core.DataAccess.Entities;
using Companies.Core.DataAccess.Interfaces;
using Companies.Core.DataAccess.Repositories;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);
// Bind section to Dictionary
//builder.Services.Configure<Dictionary<string, string>>(
//    builder.Configuration.GetSection("EmailSettings")
//);
// Add services to the container.
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
// Dependency Injection
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //}); // Enables the middleware to serve the Swagger UI (index.html)

}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
