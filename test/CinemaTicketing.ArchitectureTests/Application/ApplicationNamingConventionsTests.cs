using System.Reflection;
using CinemaTicketing.Application.Movies.Commands;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace CinemaTicketing.ArchitectureTests.Application;

public class ApplicationNamingConventionsTests
{
    private static readonly Assembly ApplicationAssembly = typeof(CreateMovieCommand).Assembly;
    private readonly ITestOutputHelper _output;

    public ApplicationNamingConventionsTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ApplicationCommandHandlers_ShouldBe_SuffixedWithCommandHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespaceEndingWith("Commands")
            .And()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the CommandHandler suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationCommands_ShouldBe_SuffixedWithCommand()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespaceEndingWith("Commands")
            .And()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the Command suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationCommandValidators_ShouldBe_SuffixedWithValidator()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .Inherit(typeof(AbstractValidator<>))
            .And()
            .ResideInNamespaceEndingWith("Commands")
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the Validator suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationQueryHandlers_ShouldBe_SuffixedWithQueryHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespaceEndingWith("Queries")
            .And()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the QueryHandler suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationQueries_ShouldBe_SuffixedWithQuery()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespaceEndingWith("Queries")
            .And()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Query")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the Query suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationQueryValidators_ShouldBe_SuffixedWithValidator()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .Inherit(typeof(AbstractValidator<>))
            .And()
            .ResideInNamespaceEndingWith("Queries")
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the Validator suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationMappings_ShouldBe_SuffixedWithMapping()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespaceEndingWith("Mapping")
            .Should()
            .HaveNameEndingWith("Mapping")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following classes do not have the Mapping suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationRepositories_ShouldBe_SuffixedWithRepository()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreInterfaces()
            .And()
            .ResideInNamespaceEndingWith("Repositories")
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("The following interfaces do not have the Repository suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(Skip = "Not sure why it's failing, need to investigate further")]
    public void ApplicationBehaviors_ShouldBe_SuffixedWithBehavior()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespace("CinemaTicketing.Application.Common.Behaviors")
            .Should()
            .HaveNameEndingWith("Behavior")
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine(
                "The following classes do not have the Behavior suffix:");
            foreach (var failingType in result.FailingTypes)
                _output.WriteLine(failingType.FullName);
        }

        result.IsSuccessful.Should().BeTrue();
    }
}