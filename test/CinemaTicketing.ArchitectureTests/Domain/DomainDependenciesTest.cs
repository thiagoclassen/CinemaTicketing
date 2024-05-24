using System.Reflection;
using CinemaTicketing.Domain.Movies;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace CinemaTicketing.ArchitectureTests.Domain;

public class DomainDependenciesTest
{
    private static readonly Assembly DomainAssembly = typeof(Movie).Assembly;
    private readonly ITestOutputHelper _output;

    public DomainDependenciesTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Domain_ShouldNot_HaveDependencies()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("Application", "Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}