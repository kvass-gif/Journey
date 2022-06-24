namespace Journey.Application.Models.City;

public class UpdateCityModel
{
    public Guid TodoListId { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public bool IsDone { get; set; }
}
