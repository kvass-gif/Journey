using Journey.Console;

var unitOfWork = Configuration.CreateUnitOfWork(args);

var list = await unitOfWork.PlaceRepo.GetAllAsync(a => a.PlaceName.Contains("m"));
foreach (var item in list)
{
    Console.WriteLine(item.PlaceName);
}
Console.WriteLine();
list = await unitOfWork.PlaceRepo.GetAllAsync(a => a.PlaceName.Contains("Lem"));
foreach (var item in list)
{
    Console.WriteLine(item.PlaceName);
}
Console.WriteLine();
list = await unitOfWork.PlaceRepo.GetAllAsync(a => a.PlaceName.Contains("Lm"));
if(list == null)
{
    Console.WriteLine("list == null");
}
foreach (var item in list)
{
    Console.WriteLine(item.PlaceName);
}
