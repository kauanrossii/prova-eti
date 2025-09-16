using Eti.TravelManager.Application.Common.Dtos;

namespace Eti.TravelManager.Application.UseCases.Destinos.PesquisarDestinos;

public class PesquisarDestinosQuery
{
    public PaginacaoDto Paginacao { get; set; } = new();
    public PesquisarDestinosQueryFilters? Filtros { get; set; }

    public record PesquisarDestinosQueryFilters(
        string Nome);
}