using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Dtos.Destinos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;

namespace Eti.TravelManager.Application.UseCases.Destinos.DetalharDestino;

public class DetalharDestinoQueryHandler : IUseCase<DetalharDestinoQuery, DestinoDto>
{
    private readonly ApplicationDbContext _context;

    public DetalharDestinoQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<DestinoDto>> ExecutarAsync(DetalharDestinoQuery requisicao, CancellationToken cancellationToken)
    {
        var destinoCadastrado = await _context.Destinos.FindAsync(requisicao.Id, cancellationToken);

        if (destinoCadastrado is null)
        {
            throw new RegistroNaoEncontradoException();
        }
        
        return ResultDto<DestinoDto>.ComSucesso(DestinoDto.FromEntidade(destinoCadastrado));
    }
}
