namespace Eti.TravelManager.Application.UseCases.Destinos.AtualizarDestino;

public class AtualizarDestinoCommand
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}