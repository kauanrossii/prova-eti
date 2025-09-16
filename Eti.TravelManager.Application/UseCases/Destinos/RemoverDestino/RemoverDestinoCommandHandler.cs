using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;

namespace Eti.TravelManager.Application.UseCases.Destinos.RemoverDestino;

public class RemoverDestinoCommandHandler : IUseCase<RemoverDestinoCommand>
{
    private readonly ApplicationDbContext _context;

    public RemoverDestinoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ExecutarAsync(RemoverDestinoCommand requisicao, CancellationToken cancellationToken)
    {
        var destinoCadastrado = await  _context.Destinos.FindAsync(requisicao.Id);

        if (destinoCadastrado == null)
        {
            throw new RegistroNaoEncontradoException();
        }
        
        _context.Destinos.Remove(destinoCadastrado);
        await _context.SaveChangesAsync(cancellationToken);
    }
}