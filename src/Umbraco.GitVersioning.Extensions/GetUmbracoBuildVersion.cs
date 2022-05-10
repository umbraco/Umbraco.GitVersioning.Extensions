using Microsoft.Build.Framework;

namespace Umbraco.GitVersioning.Extensions;

public class GetUmbracoBuildVersion : Microsoft.Build.Utilities.Task
{
    public bool PublicRelease { get; set; }

    [Required]
    public string BuildVersionSimple { get; set; } = null!;

    public string? PrereleaseVersion { get; set; }

    [Required]
    public int GitVersionHeight { get; set; }

    [Required]
    public string GitCommitIdShort { get; set; } = null!;

    [Output]
    public string AssemblyInformationalVersion { get; set; } = null!;

    [Output] 
    public string CloudBuildNumber { get; private set; } = null!;

    [Output]
    public string PackageVersion { get; private set; } = null!;

    public override bool Execute()
    {
        if (PublicRelease)
        {
            AssemblyInformationalVersion = $"{BuildVersionSimple}{PrereleaseVersion}+{GitCommitIdShort}";
            CloudBuildNumber = $"{BuildVersionSimple}{PrereleaseVersion}";
            PackageVersion = $"{BuildVersionSimple}{PrereleaseVersion}";
        }
        else
        {
            var prefix = string.IsNullOrEmpty(PrereleaseVersion)
                ? string.Empty
                : PrereleaseVersion.TrimStart('-') + ".";

            AssemblyInformationalVersion = $"{BuildVersionSimple}--{prefix}preview.{GitVersionHeight}+{GitCommitIdShort}";
            CloudBuildNumber = AssemblyInformationalVersion;
            PackageVersion = AssemblyInformationalVersion.Replace("+", ".g");
        }

        return true;
    }
}
