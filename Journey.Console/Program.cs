using Journey.Console;

var unitOfWork = Configuration.CreateUnitOfWork(args);
var list = unitOfWork.PlaceRepo.GetAllAsync().Result;
foreach (var item in list)
{
    Console.WriteLine(item.PlaceName + " " + item.ApplicationUserId);
}