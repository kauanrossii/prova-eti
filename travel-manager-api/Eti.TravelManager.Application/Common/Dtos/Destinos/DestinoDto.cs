using Eti.TravelManager.Domain.Destinos.Entities;

namespace Eti.TravelManager.Application.Common.Dtos.Destinos;

public class DestinoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public static DestinoDto FromEntidade(Destino destino)
    {
        return new DestinoDto()
        {
            Id = destino.Id,
            Nome = destino.Nome,
        };
    }
}