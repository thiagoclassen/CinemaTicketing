using System.Reflection;
using CinemaTicketing.Application.Movies.Commands;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace CinemaTicketing.ArchitectureTests.Application;

public class ApplicationDependenciesTest
{
    private static readonly Assembly ApplicationAssembly = typeof(CreateMovieCommand).Assembly;
    private readonly ITestOutputHelper _output;

    public ApplicationDependenciesTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Application_ShouldNot_DependOnInfrastructure()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn("Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}