using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    public class ESLintLogParser : IBuildLogParser
    {
        private readonly Regex _issueRegex =
            new Regex(
                "^\\s+(?<lineNumber>\\d+):(?<columnNumber>\\d+)\\s*(?<issueType>\\w+)\\s*(?<message>.+?)\\s*(?<issueCode>[^\\s]*)$",
                RegexOptions.Compiled);


        public IReadOnlyList<IssueDetails> GetIssues(string buildLogFileName)
        {
            var allLines = File.ReadAllLines(buildLogFileName);
            var issues = new List<IssueDetails>();

            string currentFileName = string.Empty;

            foreach (var line in allLines)
            {
                var match = _issueRegex.Match(line);

                if (!match.Success && !string.IsNullOrWhiteSpace(line))
                {
                    currentFileName = line;
                }
                else if (match.Success)
                {
                    var lineNumber = match.Groups["lineNumber"].Value;
                    var columnNumber = match.Groups["columnNumber"].Value;
                    var issueType = match.Groups["issueType"].Value;
                    var issueCode = match.Groups["issueCode"].Value;
                    var message = match.Groups["message"].Value;

                    issues.Add(new IssueDetails(issueType, currentFileName, lineNumber, columnNumber, issueCode,
                        message));
                }
            }

            return issues;
        }
    }
}