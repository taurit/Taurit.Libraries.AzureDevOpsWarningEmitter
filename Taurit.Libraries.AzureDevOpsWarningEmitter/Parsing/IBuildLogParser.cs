using System.Collections.Generic;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    public interface IBuildLogParser
    {
        IReadOnlyList<IssueDetails> GetIssues(string buildLogFileName);
    }
}