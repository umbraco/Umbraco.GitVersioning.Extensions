using FluentAssertions;
using Xunit;

namespace Umbraco.GitVersioning.Extensions.UnitTests;

public class GetUmbracoBuildVersionTests
{
    [Theory]
    [InlineData(false, "1.2.3", "-alpha.1", 3, "abcd123", "1.2.3--alpha.1.preview.3+abcd123")]
    [InlineData(false, "1.2.3", null, 3, "abcd123", "1.2.3--preview.3+abcd123")]
    [InlineData(false, "1.2.3", "", 3, "abcd123", "1.2.3--preview.3+abcd123")]
    [InlineData(true, "1.2.3", "-alpha.1", 3, "abcd123", "1.2.3-alpha.1+abcd123")]
    [InlineData(true, "1.2.3", null, 3, "abcd123", "1.2.3+abcd123")]
    [InlineData(true, "1.2.3", "", 3, "abcd123", "1.2.3+abcd123")]
    public void Execute_ForGivenInputs_HasCorrectAssemblyInformationalVersion(
        bool publicRelease,
        string version,
        string preRelease,
        int height,
        string sha,
        string expected)
    {
        var sut = new GetUmbracoBuildVersion
        {
            PublicRelease = publicRelease,
            BuildVersionSimple = version,
            PrereleaseVersion = preRelease,
            GitVersionHeight = height,
            GitCommitIdShort = sha
        };

        sut.Execute();

        sut.AssemblyInformationalVersion.Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "1.2.3", "-alpha.1", 3, "abcd123", "1.2.3--alpha.1.preview.3+abcd123")]
    [InlineData(false, "1.2.3", null, 3, "abcd123", "1.2.3--preview.3+abcd123")]
    [InlineData(false, "1.2.3", "", 3, "abcd123", "1.2.3--preview.3+abcd123")]
    [InlineData(true, "1.2.3", "-alpha.1", 3, "abcd123", "1.2.3-alpha.1")]
    [InlineData(true, "1.2.3", null, 3, "abcd123", "1.2.3")]
    [InlineData(true, "1.2.3", "", 3, "abcd123", "1.2.3")]
    public void Execute_ForGivenInputs_HasCorrectCloudBuildNumber(
        bool publicRelease,
        string version,
        string preRelease,
        int height,
        string sha,
        string expected)
    {
        var sut = new GetUmbracoBuildVersion
        {
            PublicRelease = publicRelease,
            BuildVersionSimple = version,
            PrereleaseVersion = preRelease,
            GitVersionHeight = height,
            GitCommitIdShort = sha
        };

        sut.Execute();

        sut.CloudBuildNumber.Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "1.2.3", "-alpha.1", 3, "abcd123", "1.2.3--alpha.1.preview.3.gabcd123")]
    [InlineData(false, "1.2.3", null, 3, "abcd123", "1.2.3--preview.3.gabcd123")]
    [InlineData(false, "1.2.3", "", 3, "abcd123", "1.2.3--preview.3.gabcd123")]
    [InlineData(true, "1.2.3", "-alpha.1", 3, "abcd123", "1.2.3-alpha.1")]
    [InlineData(true, "1.2.3", null, 3, "abcd123", "1.2.3")]
    [InlineData(true, "1.2.3", "", 3, "abcd123", "1.2.3")]
    public void Execute_ForGivenInputs_HasCorrectPackageVersion(
        bool publicRelease,
        string version,
        string preRelease,
        int height,
        string sha,
        string expected)
    {
        var sut = new GetUmbracoBuildVersion
        {
            PublicRelease = publicRelease,
            BuildVersionSimple = version,
            PrereleaseVersion = preRelease,
            GitVersionHeight = height,
            GitCommitIdShort = sha
        };

        sut.Execute();

        sut.PackageVersion.Should().Be(expected);
    }
}
