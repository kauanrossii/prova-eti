using Eti.TravelManager.Application.Common.Dtos;

namespace Eti.TravelManager.Application.UseCases.Viagens.PesquisarViagens;

public class PesquisarViagensQuery
{
    public PaginacaoDto Paginacao { get; set; } = new();
    public PesquisarViagensQueryFilters? Filtros { get; set; }
    
    public record PesquisarViagensQueryFilters(
        string? Nome
    );
}