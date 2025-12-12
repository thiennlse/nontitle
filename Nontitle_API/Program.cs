using Nontitle.ServiceDefaults;
using Nontitle_API.Extensions;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Interfaces;
using Nontitle_Repository.Repositories;
using Nontitle_Service.Extensions;
using Nontitle_Service.Interfaces;
using Nontitle_Service.MapperProfile;
using Nontitle_Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// DI
builder.Services.AddControllers();
builder.Services.AddRepository();
builder.Services.AddService();
builder.Services.AddScoped<AuthExtension>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddConfigSwagger();
builder.Services.AddJwtAuthentication(builder.Configuration);
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.AddMigration();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
