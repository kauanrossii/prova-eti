using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Dtos.Destinos;
using Eti.TravelManager.Application.Common.Dtos.Viagens;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Viagens.Entities;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Viagens.PesquisarViagens;

public class PesquisarViagensQueryHandler : IUseCase<PesquisarViagensQuery, ResultadoPaginado<ViagemDto>>
{
    private readonly ApplicationDbContext _context;

    public PesquisarViagensQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<ResultadoPaginado<ViagemDto>>> ExecutarAsync(PesquisarViagensQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Viagens.AsQueryable();

        if (request.Filtros?.Nome != null)
        {
            query = query.Where(x => x.Nome.Contains(request.Filtros.Nome));
        }
        
        var resultado = await _context.Viagens
            .Skip(request.Paginacao.QuantidadeRegistros * (request.Paginacao.NumeroPagina - 1))
            .Take(request.Paginacao.QuantidadeRegistros)
            .Select(viagem => new ViagemDto()
            {
                Id = viagem.Id,
                Nome = viagem.Nome,
                DataChegada = viagem.DataChegada,
                DataSaida = viagem.DataSaida,
                Destinos = viagem.Destinos.Select(destino => new DestinoDto()
                {
                    Id = destino.Id,
                    Nome = destino.Nome,
                })
            })
            .ToListAsync(cancellationToken);

        return ResultDto<ResultadoPaginado<ViagemDto>>.ComSucesso(new ResultadoPaginado<ViagemDto>(
            request.Paginacao.NumeroPagina,
            request.Paginacao.QuantidadeRegistros,
            request.Paginacao.QuantidadeRegistros,
            resultado
        ));
    }
}