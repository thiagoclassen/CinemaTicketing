using System.Reflection;
using CinemaTicketing.Domain.Movies;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace CinemaTicketing.ArchitectureTests.Domain;

public class DomainSealedClassesTests
{
    private static readonly Assembly DomainAssembly = typeof(Movie).Assembly;
    private readonly ITestOutputHelper _output;

    public DomainSealedClassesTests(ITestOutputHelper output)
    {
        _output = output;
    }


    [Fact]
    public void DomainClasses_ShouldBe_Sealed()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .AreClasses()
            .And()
            .DoNotResideInNamespace("CinemaTicketing.Domain.Common.Errors")
            .Should()
            .BeSealed()
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes are not sealed:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }
}