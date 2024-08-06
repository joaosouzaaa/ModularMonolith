using Microsoft.Extensions.Configuration;
using ModularMonolith.Common.Factories;

namespace UnitTests.FactoriesTests.Common;

public sealed class ConnectionStringFactoryTests
{
    [Fact]
    public void GetConnectionString_SuccessfulScenario_ReturnsContainerConnection()
    {
        // A
        const string containerConnection = "test";

        var inMemoryCollection = new Dictionary<string, string>()
        {
            {"ConnectionStrings:ContainerConnection", containerConnection }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryCollection!)
            .Build();

        Environment.SetEnvironmentVariable("DOCKER_ENVIROMENT", "DockerDevelopment");

        // A
        var connectionStringResult = configuration.GetConnectionString();

        // A
        Assert.Equal(containerConnection, connectionStringResult);
    }

    [Fact]
    public void GetConnectionString_SuccessfulScenario_ReturnsLocalConnection()
    {
        // A
        const string localConnection = "joao";

        var inMemoryCollection = new Dictionary<string, string>()
        {
            {"ConnectionStrings:LocalConnection", localConnection}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryCollection!)
            .Build();

        Environment.SetEnvironmentVariable("DOCKER_ENVIROMENT", "any value");

        // A
        var connectionStringResult = configuration.GetConnectionString();

        // A
        Assert.Equal(localConnection, connectionStringResult);
    }
}
