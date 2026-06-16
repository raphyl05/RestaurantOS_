using Microsoft.EntityFrameworkCore;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.Services;
using RestaurantOS.Domain.Interfaces;
using RestaurantOS.Infrastructure.Data;
using RestaurantOS.Infrastructure.Repositories;
using RestaurantOS.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// API
builder.Services.AddControllers();

// Base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios (Domain <- Infrastructure)
builder.Services.AddScoped<IMesaRepository, MesaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();

// Seguridad
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

// Servicios de aplicación (Application <- Application)
builder.Services.AddScoped<IMesaService, MesaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPagoService, PagoService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();