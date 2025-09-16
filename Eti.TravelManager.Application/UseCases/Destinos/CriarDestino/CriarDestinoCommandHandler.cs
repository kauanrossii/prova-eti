using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Destinos.Entities;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Destinos.CriarDestino;

public class CriarDestinoCommandHandler : IUseCase<CriarDestinoCommand, EntityIdDto>
{
    private readonly ApplicationDbContext _context;

    public CriarDestinoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<EntityIdDto>> ExecutarAsync(CriarDestinoCommand requisicao, CancellationToken cancellationToken)
    {
        var novoDestino = new Destino(requisicao.Nome);
        var destinoDuplicado = await _context.Destinos.AnyAsync(destino => destino.Nome.Contains(novoDestino.Nome), cancellationToken);

        if (destinoDuplicado)
        {
            return ResultDto<EntityIdDto>.ComErro(["Já existe um destino cadastrado de mesmo nome."]);
        } 
        
        _context.Destinos.Add(novoDestino);
        await _context.SaveChangesAsync(cancellationToken);
        return ResultDto<EntityIdDto>.ComSucesso(new EntityIdDto(novoDestino.Id));
    }
}