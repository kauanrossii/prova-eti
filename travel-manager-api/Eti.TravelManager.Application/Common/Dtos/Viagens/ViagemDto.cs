using Eti.TravelManager.Application.Common.Dtos.Destinos;
using Eti.TravelManager.Domain.Viagens.Entities;

namespace Eti.TravelManager.Application.Common.Dtos.Viagens;

public class ViagemDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTimeOffset DataSaida { get; set; }
    public DateTimeOffset DataChegada { get; set; }
    public decimal Valor { get; set; }
    public IEnumerable<DestinoDto> Destinos { get; set; } = [];
    
    public ViagemDto() { }

    public static ViagemDto FromEntidade(Viagem viagem)
    {
        return new ViagemDto
        {
            Id = viagem.Id,
            Nome = viagem.Nome,
            DataSaida = viagem.DataSaida,
            DataChegada = viagem.DataChegada,
            Valor = viagem.Valor,
            Destinos = viagem.Destinos.Select(destino => DestinoDto.FromEntidade(destino)).ToList()
        };
    } 
}