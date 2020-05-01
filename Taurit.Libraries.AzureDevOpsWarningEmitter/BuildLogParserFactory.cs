using System;
using Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter
{
    public static class BuildLogParserFactory
    {
        public static IBuildLogParser GetParser(BuildLogParserType parserType)
        {
            switch (parserType)
            {
                case BuildLogParserType.Dotnet:
                    return new DotnetBuildLogParser();
                case BuildLogParserType.Npm:
                    return new NpmBuildLogParser();
                default:
                    throw new InvalidOperationException($"Requested parser type `{parserType}` is not recognized.");
            }
        }

        public static IBuildLogParser GetParser(string parserType)
        {
            switch (parserType)
            {
                case "dotnet":
                    return new DotnetBuildLogParser();
                case "npm":
                    return new NpmBuildLogParser();
                default:
                    throw new InvalidOperationException($"Requested parser type `{parserType}` is not recognized.");
            }
        }
    }
}