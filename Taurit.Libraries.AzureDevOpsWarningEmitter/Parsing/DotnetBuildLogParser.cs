using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    /// <summary>
    ///     Parses a line from the "dotnet build" output and creates an object model of an issue like warning or error.
    /// </summary>
    internal class DotnetBuildLogParser : IBuildLogParser
    {
        // An example of a parsed line:
        // Id\IId.cs(7,14): warning CS0108: 'IId.Equals(IId)' hides inherited member 'IEquatable<IId>.Equals(IId)'. Use the new keyword if hiding was intended. [d:\x.csproj]
        private readonly Regex _issueCodeRegex = new Regex("(?<=(warning|error) )[A-Za-z0-9]+");
        private readonly Regex _issueLocationRegex = new Regex("(\\d+,\\d+)");
        private readonly Regex _issueTypeRegex = new Regex("\\): (?<issueType>warning|error)");
        private readonly Regex _messageRegex = new Regex("(?<=((warning|error) [A-Za-z0-9]+: )).*");
        private readonly Regex _sourcePathRegex = new Regex("^[^(]*");

        public IReadOnlyList<IssueDetails> GetIssues(string buildLogFileName)
        {
            var allLines = File.ReadLines(buildLogFileName);
            var issues = allLines.Where(DoesLineContainIssue).Select(ParseIssueLine).ToList();
            return issues;
        }

        /// <summary>
        ///     A quick test to see if a line contains warning or error
        /// </summary>
        private bool DoesLineContainIssue(string line)
        {
            return _issueTypeRegex.Match(line).Success;
        }

        /// <remarks>
        ///     Regexes are based on https://github.com/dotwhat/azure-devops-dotnet-warnings
        /// </remarks>
        private IssueDetails ParseIssueLine(string line)
        {
            var issueType = _issueTypeRegex.Match(line).Groups["issueType"].Value;
            var issueLocation = _issueLocationRegex.Match(line).Groups[0].Value.Split(',');
            var sourcePath = _sourcePathRegex.Match(line).Groups[0].Value.Replace('/', '\\');
            var lineNumber = issueLocation[0];
            var columnNumber = issueLocation[1];

            var issueCode = _issueCodeRegex.Match(line).Groups[0].Value;
            var message = _messageRegex.Match(line).Groups[0].Value;

            return new IssueDetails(issueType, sourcePath, lineNumber, columnNumber, issueCode, message);
        }
    }
}