using Journey.DataAccess.IntegrationTests.Config;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Tests;

[TestFixture]
public class PlaceRepositoryTests : BaseOneTimeSetup
{
    [Test]
    public Task GetFirstAsyncTest()
    {
        return Task.CompletedTask;
    }
}
