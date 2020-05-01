using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    /// <summary>
    ///     Parses a line from the "dotnet build" output and creates an object model of an issue like warning or error.
    /// </summary>
    internal class NpmBuildLogParser : IBuildLogParser
    {
        private readonly Regex _messageRegex = new Regex("(?<=((warning|error) [A-Za-z0-9]+: )).*");

        public IEnumerable<IssueDetails> GetIssues(string buildLogFileName)
        {
            var result = new[]
            {
                new IssueDetails("Asd", "gh", "dgf", "dfg", "fgh", "fgh")
            };
            return result;
        }
    }
}