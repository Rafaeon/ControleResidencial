using AutoMapper;
using ControleResidencial.Infra;
using ControleResidencial.Infra.Repository;
using ControleResidencial.Infra.Repository.Entity;
using ControleResidencial.Infra.Repository.Interfaces;
using ControleResidencial.Mappings;
using ControleResidencial.Services.Categoria;
using ControleResidencial.Services.Categoria.Interface;
using ControleResidencial.Services.Transacao;
using ControleResidencial.Services.Transacao.Interface;
using ControleResidencial.Services.Usuario;
using ControleResidencial.Services.Usuario.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();



builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<ITransacaoServices, TransacaoServices>();
builder.Services.AddScoped<ICategoriaServices, CategoriaServices>();

builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowReact");

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
