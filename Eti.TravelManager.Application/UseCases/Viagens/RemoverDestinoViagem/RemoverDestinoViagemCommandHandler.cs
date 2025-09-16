using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;

namespace Eti.TravelManager.Application.UseCases.Viagens.RemoverDestinoViagem;

public class RemoverDestinoViagemCommandHandler : IUseCase<RemoverDestinoViagemCommand>
{
    private readonly ApplicationDbContext _context;

    public RemoverDestinoViagemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ExecutarAsync(RemoverDestinoViagemCommand requisicao, CancellationToken cancellationToken)
    {
        var viagemCadastrada = await _context.Viagens.FindAsync([requisicao.Id], cancellationToken: cancellationToken);

        if (viagemCadastrada == null)
        {
            throw new RegistroNaoEncontradoException();
        }

        var destinoCadastrado =
            await _context.Destinos.FindAsync([requisicao.DestinoId], cancellationToken: cancellationToken);

        if (destinoCadastrado is null)
        {
            throw new RegistroNaoEncontradoException();
        }

        viagemCadastrada.RemoveDestino(destinoCadastrado);
        await _context.SaveChangesAsync(cancellationToken);
    }
}