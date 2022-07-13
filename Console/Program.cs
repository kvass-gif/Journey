using Journey.Console;
using System.Text.Json;

var unitOfWork = Configuration.CreateUnitOfWork(args);
var list = unitOfWork.PlaceRepo.GetAllAsync().Result;
foreach (var item in list)
{
    Console.WriteLine(item.PlaceName);
}
string jsonString = JsonSerializer.Serialize(list);
Console.WriteLine(jsonString);