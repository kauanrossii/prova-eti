using Eti.TravelManager.Application.UseCases.Destinos.AtualizarDestino;
using Eti.TravelManager.Application.UseCases.Destinos.CriarDestino;
using Eti.TravelManager.Application.UseCases.Destinos.DetalharDestino;
using Eti.TravelManager.Application.UseCases.Destinos.PesquisarDestinos;
using Eti.TravelManager.Application.UseCases.Destinos.RemoverDestino;
using Eti.TravelManager.Application.UseCases.Viagens.AdicionarDestino;
using Eti.TravelManager.Application.UseCases.Viagens.AtualizarViagem;
using Eti.TravelManager.Application.UseCases.Viagens.CriarViagem;
using Eti.TravelManager.Application.UseCases.Viagens.DetalharViagem;
using Eti.TravelManager.Application.UseCases.Viagens.PesquisarViagens;
using Eti.TravelManager.Application.UseCases.Viagens.RemoverDestinoViagem;
using Eti.TravelManager.Application.UseCases.Viagens.RemoverViagem;
using Eti.TravelManager.Extensions;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<CriarDestinoCommandHandler>();
builder.Services.AddScoped<DetalharDestinoQueryHandler>();
builder.Services.AddScoped<PesquisarDestinosQueryHandler>();
builder.Services.AddScoped<AtualizarDestinoCommandHandler>();
builder.Services.AddScoped<RemoverDestinoCommandHandler>();

builder.Services.AddScoped<CriarViagemCommandHandler>();
builder.Services.AddScoped<DetalharViagemQueryHandler>();
builder.Services.AddScoped<PesquisarViagensQueryHandler>();
builder.Services.AddScoped<AtualizarViagemCommandHandler>();
builder.Services.AddScoped<RemoverViagemCommandHandler>();
builder.Services.AddScoped<AdicionarDestinoViagemCommandHandler>();
builder.Services.AddScoped<RemoverDestinoViagemCommandHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
