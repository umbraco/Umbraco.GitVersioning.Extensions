using System;
using Microsoft.Build.Framework;

namespace Umbraco.GitVersioning.Extensions
{
    public class GetUmbracoBuildVersion : Microsoft.Build.Utilities.Task
    {
        public bool PublicRelease { get; set; }

        [Required]
        public string BuildVersionSimple { get; set; }

        public string PrereleaseVersion { get; set; }

        [Required]
        public int GitVersionHeight { get; set; }

        [Required]
        public string GitCommitIdShort { get; set; }

        [Output]
        public string AssemblyInformationalVersion { get; set; }

        [Output]
        public string CloudBuildNumber { get; private set; }

        [Output]
        public string PackageVersion { get; private set; }

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

            Console.WriteLine($"##vso[task.setvariable variable=GitAssemblyInformationalVersion;]{PackageVersion}");

            return true;
        }
    }

}
