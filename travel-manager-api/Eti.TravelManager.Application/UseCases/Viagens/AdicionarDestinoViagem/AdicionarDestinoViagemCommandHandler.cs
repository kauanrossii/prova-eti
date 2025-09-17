using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;

namespace Eti.TravelManager.Application.UseCases.Viagens.AdicionarDestino;

public class AdicionarDestinoViagemCommandHandler : IUseCase<AdicionarDestinoViagemCommand, EntityIdDto>
{
    private readonly ApplicationDbContext _context;

    public AdicionarDestinoViagemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<EntityIdDto>> ExecutarAsync(AdicionarDestinoViagemCommand requisicao,
        CancellationToken cancellationToken)
    {
        var viagemCadastrada = await _context.Viagens.FindAsync([requisicao.Id], cancellationToken);

        if (viagemCadastrada == null)
        {
            throw new RegistroNaoEncontradoException();
        }

        var destinoCadastrado = await _context.Destinos.FindAsync([requisicao.DestinoId], cancellationToken);

        if (destinoCadastrado is null)
        {
            throw new RegistroNaoEncontradoException();
        }

        var sucessoOperacao = viagemCadastrada.AddDestino(destinoCadastrado);
        if (!sucessoOperacao)
        {
            return ResultDto<EntityIdDto>.ComErro(["O destino selecionado já foi adicionado à viagem."]);
        }
        
        _context.Viagens.Update(viagemCadastrada);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultDto<EntityIdDto>.ComSucesso(new EntityIdDto(destinoCadastrado.Id));
    }
}