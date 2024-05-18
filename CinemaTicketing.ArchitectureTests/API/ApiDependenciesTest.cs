using System.Reflection;
using CinemaTicketing.API.Controllers;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace CinemaTicketing.ArchitectureTests.API;

public class ApiDependenciesTest
{
    private static readonly Assembly ApiAssembly = typeof(MoviesController).Assembly;
    private readonly ITestOutputHelper _output;

    public ApiDependenciesTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ApiControllers_ShouldNot_ReferenceInfrastructure()
    {
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .ShouldNot()
            .HaveDependencyOn("CinemaTicketing.Infrastructure.*")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes violate the dependency policy:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApiControllers_ShouldNot_ReferenceDomain()
    {
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .ShouldNot()
            .HaveDependencyOn("CinemaTicketing.Domain.*")
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