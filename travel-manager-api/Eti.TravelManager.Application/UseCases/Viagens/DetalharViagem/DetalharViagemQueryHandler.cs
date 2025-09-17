using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Dtos.Viagens;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Viagens.DetalharViagem;

public class DetalharViagemQueryHandler : IUseCase<DetalharViagemQuery, ViagemDto>
{
    private readonly ApplicationDbContext _context;

    public DetalharViagemQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<ViagemDto>> ExecutarAsync(DetalharViagemQuery requisicao, CancellationToken cancellationToken)
    {
        var viagem = await _context.Viagens
            .Include(viagem => viagem.Destinos)
            .SingleOrDefaultAsync(cancellationToken);

        if (viagem is null)
        {
            throw new RegistroNaoEncontradoException();
        }
        
        return ResultDto<ViagemDto>.ComSucesso(
            ViagemDto.FromEntidade(viagem)
            );
    }
}