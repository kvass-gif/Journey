using Journey.DataAccess.Entities;
using Journey.DataAccess.IntegrationTests.Config;
using Journey.DataAccess.Repositories;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Tests;

[TestFixture]
public class PlaceRepositoryTests : BaseOneTimeSetup
{
    [Test]
    public async Task GetFirstAsyncTest()
    {
        // Arrange
        IPlaceRepository placeRepository = unitOfWork.PlaceRepo;
        // Act
        Place place = await placeRepository.GetFirstAsync(a => a.PlaceName == "place1");
        // Assert
        Assert.NotNull(place);
    }
}
