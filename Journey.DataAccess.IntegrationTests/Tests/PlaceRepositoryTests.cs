using Journey.DataAccess.Entities;
using Journey.DataAccess.IntegrationTests.Config;
using Journey.DataAccess.Repositories;
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
