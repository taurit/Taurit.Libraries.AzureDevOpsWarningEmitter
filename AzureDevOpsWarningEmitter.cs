using System;
using System.Collections.Generic;
using Taurit.Libraries.AzureDevOpsWarningEmitter.Emitting;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter
{
    public class AzureDevOpsWarningEmitter
    {
        private readonly IEnumerable<IssueDetails> _issues;

        public AzureDevOpsWarningEmitter(IEnumerable<IssueDetails> issues)
        {
            _issues = issues;
        }

        public void EmitWarningsAndErrors()
        {
            var taskResult = new DevOpsTaskResult();

            foreach (var issue in _issues)
            {
                taskResult.RegisterIssue(issue);

                // emit a warning or an error
                Console.WriteLine(issue);
            }

            // set status of a task by invoking a logging command
            Console.WriteLine(taskResult);
        }
    }
}