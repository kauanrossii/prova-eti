using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Dtos.Destinos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Destinos.PesquisarDestinos;

public class PesquisarDestinosQueryHandler : IUseCase<PesquisarDestinosQuery, ResultadoPaginado<DestinoDto>>
{
    private readonly ApplicationDbContext _context;

    public PesquisarDestinosQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<ResultadoPaginado<DestinoDto>>> ExecutarAsync(PesquisarDestinosQuery requisicao, CancellationToken cancellationToken)
    {
        var query = _context.Destinos.AsQueryable();

        if (requisicao.Filtros?.Nome != null)
        {
            query = query.Where(x => x.Nome.Contains(requisicao.Filtros.Nome));
        }

        var totalRegistros = await query.CountAsync(cancellationToken: cancellationToken);
        
        var resultado = await _context.Destinos
            .Skip(requisicao.Paginacao.QuantidadeRegistros * (requisicao.Paginacao.NumeroPagina - 1))
            .Take(requisicao.Paginacao.QuantidadeRegistros)
            .Select(destino => new DestinoDto()
            {
                Id = destino.Id,
                Nome = destino.Nome,
            })
            .ToListAsync(cancellationToken);

        return ResultDto<ResultadoPaginado<DestinoDto>>.ComSucesso(new ResultadoPaginado<DestinoDto>(
            requisicao.Paginacao.NumeroPagina,
            requisicao.Paginacao.QuantidadeRegistros,
            totalRegistros,
            resultado
        ));
    }
}