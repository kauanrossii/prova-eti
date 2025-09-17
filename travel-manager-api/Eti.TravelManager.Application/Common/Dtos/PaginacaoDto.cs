namespace Eti.TravelManager.Application.Common.Dtos;

public class PaginacaoDto
{
    public int NumeroPagina { get; set; } = 1;
    public int QuantidadeRegistros { get; set; } = 10;
}