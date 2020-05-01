using System.Collections.Generic;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    /// <summary>
    ///     Parses a line from the "dotnet build" output and creates an object model of an issue like warning or error.
    /// </summary>
    internal class NpmBuildLogParser : IBuildLogParser
    {
        public IReadOnlyList<IssueDetails> GetIssues(string buildLogFileName)
        {
            var result = new[]
            {
                new IssueDetails("Asd", "gh", "dgf", "dfg", "fgh", "fgh")
            };
            return result;
        }
    }
}