using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;

namespace Eti.TravelManager.Application.UseCases.Viagens.RemoverViagem;

public class RemoverViagemCommandHandler : IUseCase<RemoverViagemCommand>
{
    private readonly ApplicationDbContext _context;

    public RemoverViagemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Task ExecutarAsync(RemoverViagemCommand requisicao, CancellationToken cancellationToken)
    {
        var viagem = _context.Viagens.SingleOrDefault(v => v.Id == requisicao.Id);

        if (viagem == null)
        {
            throw new RegistroNaoEncontradoException();
        }
        
        _context.Viagens.Remove(viagem);
        return _context.SaveChangesAsync(cancellationToken);
    }
}
