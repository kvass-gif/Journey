using Journey.DataAccess.IntegrationTests.Config;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Tests;

[TestFixture]
public class PlaceRepositoryTests : BaseOneTimeSetup
{
    [Test]
    public async Task GetAllByNameAsyncTest()
    {
        var placeRepository = unitOfWork.PlaceRepo;
        var list = await placeRepository.GetAllByName("m", 5, 10);
        Assert.True(true);
        
    }
}
