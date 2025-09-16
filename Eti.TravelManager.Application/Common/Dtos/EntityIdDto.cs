namespace Eti.TravelManager.Application.Common.Dtos;

public class EntityIdDto
{
    public int Id { get; set; }
    
    public EntityIdDto() { }

    public EntityIdDto(int id)
    {
        Id = id;
    }
}