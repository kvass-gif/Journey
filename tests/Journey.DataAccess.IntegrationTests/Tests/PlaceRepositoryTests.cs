using Journey.DataAccess.IntegrationTests.Config;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Tests;

[TestFixture]
public class PlaceRepositoryTests : BaseOneTimeSetup
{
    [Test]
    public async Task GetAllAsyncTest()
    {
        var placeRepository = unitOfWork.PlaceRepo;
        var listM = await placeRepository.GetAllAsync(a => a.PlaceName.Contains("m"));
        var listLem = await placeRepository.GetAllAsync(a => a.PlaceName.Contains("Lem"));
        var listLm = await placeRepository.GetAllAsync(a => a.PlaceName.Contains("Lm"));
        Assert.True(listM.Count == 2 && listLem.Count == 1 && listLm.Count == 0);
    }
}
