// Program.cs
using Microsoft.EntityFrameworkCore;
using lab13.Models; // Tu DbContext está aquí (LinqexampleContext)
using lab13.Repositories; // Tus repositorios
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la Conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ¡IMPORTANTE! Usa el DbContext que generó Scaffolding: LinqexampleContext
builder.Services.AddDbContext<LinqexampleContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// 2. Registrar los Repositorios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// 3. Registrar MediatR (CQRS)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// 4. Servicios de API y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Configurar el pipeline de HTTP
// ASÍ DEBE QUEDAR (Saca las llaves del if)
// if (app.Environment.IsDevelopment())  <-- Puedes comentar o borrar el if
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();