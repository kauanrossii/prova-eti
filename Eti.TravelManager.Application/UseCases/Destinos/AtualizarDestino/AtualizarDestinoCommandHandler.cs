using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Destinos.AtualizarDestino;

public class AtualizarDestinoCommandHandler : IUseCase<AtualizarDestinoCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public AtualizarDestinoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<bool>> ExecutarAsync(AtualizarDestinoCommand requisicao, CancellationToken cancellationToken)
    {
        var destinoAntigo = await _context.Destinos.FindAsync(requisicao.Id);

        if (destinoAntigo is null)
        {
            throw new RegistroNaoEncontradoException();
        }

        var destinoDuplicado =
            await _context.Destinos.AnyAsync(destino => destino.Id != requisicao.Id && destino.Nome == requisicao.Nome);

        if (destinoDuplicado)
        {
            return ResultDto<bool>.ComErro(["Já existe outro destino cadastrado de mesmo nome."]);
        }

        destinoAntigo.AtualizarInformacoes(requisicao.Nome);
        await _context.SaveChangesAsync();
        return ResultDto<bool>.ComSucesso(true);
    }
}
