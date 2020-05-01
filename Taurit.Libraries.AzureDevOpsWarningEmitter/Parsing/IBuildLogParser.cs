using System.Collections.Generic;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Parsing
{
    public interface IBuildLogParser
    {
        IEnumerable<IssueDetails> GetIssues(string buildLogFileName);
    }
}