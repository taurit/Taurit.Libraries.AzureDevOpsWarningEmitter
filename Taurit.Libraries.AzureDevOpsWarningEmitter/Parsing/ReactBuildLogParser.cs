using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    /// <summary>
    ///     Parses a line from the "dotnet build" output and creates an object model of an issue like warning or error.
    /// </summary>
    internal class ReactBuildLogParser : IBuildLogParser
    {
        private Regex warningLineRegex = new Regex("^\\s+Line (?<lineNumber>\\d+):(?<columnNumber>\\d+):\\s*(?<message>[^@]*)(?<issueCode>.*)$", RegexOptions.Compiled);

        public IReadOnlyList<IssueDetails> GetIssues(string buildLogFileName)
        {
            var allLines = File.ReadAllLines(buildLogFileName);
            var issues = new List<IssueDetails>();

            bool inWarningSection = false;
            string currentFileName = String.Empty;

            foreach (var line in allLines)
            {
                if (line.Contains("Compiled with warnings"))
                {
                    inWarningSection = true;
                    continue;
                }

                if (inWarningSection)
                {
                    if (String.IsNullOrWhiteSpace(line)) continue;
                    if (!line.StartsWith("  Line "))
                    {
                        currentFileName = line;
                        continue;
                    }
                    // otherwise we expect line like:
                    // "  Line 4:13:   'Component1Actions' is defined but never used             @typescript-eslint/no-unused-vars"
                    var match = warningLineRegex.Match(line);

                    var lineNumber = match.Groups["lineNumber"].Value;
                    var columnNumber = match.Groups["columnNumber"].Value;
                    var issueCode = match.Groups["issueCode"].Value;
                    var message = match.Groups["message"].Value;
                    
                    issues.Add(new IssueDetails("warning", currentFileName, lineNumber, columnNumber, issueCode, message));
                }

            }

            return issues;
        }
    }
}