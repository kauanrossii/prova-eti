using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Viagens.Entities;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Viagens.CriarViagem;

public class CriarViagemCommandHandler : IUseCase<CriarViagemCommand, EntityIdDto>
{
    private readonly ApplicationDbContext _context;

    public CriarViagemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<EntityIdDto>> ExecutarAsync(CriarViagemCommand requisicao, CancellationToken cancellationToken)
    {
        var viagem = new Viagem(requisicao.Nome, requisicao.DataSaida, requisicao.DataChegada, requisicao.Valor);
        
        var viagemDuplicada = await _context.Viagens.FirstOrDefaultAsync(v => v.Nome == viagem.Nome, cancellationToken: cancellationToken);

        if (viagemDuplicada is not null)
        {
            return ResultDto<EntityIdDto>.ComErro(["Já existe uma viagem cadastrada de mesmo nome"]);
        }
        
        _context.Viagens.Add(viagem);
        await _context.SaveChangesAsync(cancellationToken);
        
        return ResultDto<EntityIdDto>.ComSucesso(new EntityIdDto(viagem.Id));
    }
}