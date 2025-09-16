namespace Eti.TravelManager.Application.Common.Dtos;

public class ResultadoPaginado<T>
{
    public int NumeroPagina { get; set; }
    public int QuantidadeRegistros { get; set; }
    public int TotalRegistros { get; set; }
    public IEnumerable<T> Dados { get; set; }

    public ResultadoPaginado(int numeroPagina, int quantidadeRegistros, int totalRegistros, IEnumerable<T> dados)
    {
        NumeroPagina = numeroPagina;
        QuantidadeRegistros = quantidadeRegistros;
        TotalRegistros = totalRegistros;
        Dados = dados;
    }
}