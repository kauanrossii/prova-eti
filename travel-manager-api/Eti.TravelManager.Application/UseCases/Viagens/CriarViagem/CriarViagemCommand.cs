namespace Eti.TravelManager.Application.UseCases.Viagens.CriarViagem;

public class CriarViagemCommand
{
    public required string Nome { get; set; }
    public required DateTimeOffset DataSaida { get; set; }
    public required DateTimeOffset DataChegada { get; set; }
    public required decimal Valor { get; set; }
}