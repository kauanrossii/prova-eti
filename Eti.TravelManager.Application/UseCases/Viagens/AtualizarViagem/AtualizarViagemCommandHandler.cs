using Eti.TravelManager.Application.Common;
using Eti.TravelManager.Application.Common.Dtos;
using Eti.TravelManager.Application.Common.Interfaces;
using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Application.UseCases.Viagens.AtualizarViagem;

public class AtualizarViagemCommandHandler : IUseCase<AtualizarViagemCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public AtualizarViagemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<bool>> ExecutarAsync(AtualizarViagemCommand requisicao, CancellationToken cancellationToken)
    {
        var viagemCadastrada = await _context.Viagens.FindAsync(requisicao.Id);

        if (viagemCadastrada is null)
        {
            throw new RegistroNaoEncontradoException();
        }
        
        var viagemDuplicada = await _context.Viagens.AnyAsync(viagem => viagem.Id != requisicao.Id && viagem.Nome == requisicao.Nome);

        if (viagemDuplicada)
        {
            return ResultDto<bool>.ComErro(["Já existe outra viagem cadastrada de mesmo nome"]);
        }
        
        viagemCadastrada.AtualizarInformacoes(requisicao.Nome, requisicao.DataSaida, requisicao.DataChegada, requisicao.Valor);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultDto<bool>.ComSucesso(true);
    }
}