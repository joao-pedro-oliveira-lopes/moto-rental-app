using Microsoft.EntityFrameworkCore;
using MotoRentalApp.Infrastructure.Data;
using MotoRentalApp.Infrastructure.Repositories;
using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Enums;
using MotoRentalApp.Application.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MotoRentalApp.Application.Interfaces.Messaging;
using MotoRentalApp.Infrastructure.Messaging;
using MotoRentalApp.Application.Interfaces.Security;
using MotoRentalApp.Infrastructure.Security;
using MotoRentalApp.Application.Events;
using MotoRentalApp.Api.Config;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Configuração de Autenticação e Autorização
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("EntregadorOnly", policy => policy.RequireRole(UserRole.Entregador.ToString()));
});

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();
// Registro de repositórios e dependências
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IEventConsumer<Vehicle2024Event>, Vehicle2024EventConsumer>();

// Configuração do RabbitMqHostedService como Singleton
builder.Services.AddSingleton<RabbitMqHostedService>();
builder.Services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<RabbitMqHostedService>());

// Configuração do RabbitMqEventPublisher
builder.Services.AddScoped<IEventPublisher, RabbitMqEventPublisher>(provider =>
{
    var configuration = builder.Configuration.GetSection("RabbitMQ");
    var logger = provider.GetRequiredService<ILogger<RabbitMqEventPublisher>>();

    return new RabbitMqEventPublisher(
        configuration["HostName"],
        configuration["QueueName"],
        logger
    );
});

// Configuração de serviços adicionais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileOperationFilter>();
});

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
