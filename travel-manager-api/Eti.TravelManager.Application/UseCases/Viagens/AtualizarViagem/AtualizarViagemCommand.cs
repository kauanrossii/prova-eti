namespace Eti.TravelManager.Application.UseCases.Viagens.AtualizarViagem;

public class AtualizarViagemCommand
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTimeOffset DataChegada { get; set; }
    public DateTimeOffset DataSaida { get; set; }
    public decimal Valor { get; set; }
}