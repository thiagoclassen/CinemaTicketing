using System.Reflection;
using CinemaTicketing.Infrastructure.Users.Persistence;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace CinemaTicketing.ArchitectureTests.Infrastructure;

public class InfrastructureDependenciesTest
{
    private static readonly Assembly InfrastructureAssembly = typeof(UserRepository).Assembly;
    private readonly ITestOutputHelper _output;

    public InfrastructureDependenciesTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void InfrastructureRepositories_Should_HaveDependencieOnDomain()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .AreClasses()
            .And()
            .HaveNameEndingWith("Repository")
            .Should()
            .HaveDependencyOnAny("CinemaTicketing.Domain", "CinemaTicketing.Application")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes violate the dependency policy:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }
}