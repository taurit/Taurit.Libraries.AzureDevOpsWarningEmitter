namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Emitting
{
    internal class DevOpsTaskResult
    {
        private int _numErrors;
        private int _numWarnings;
        private DevOpsTaskResultStatus _status = DevOpsTaskResultStatus.Succeeded;

        public void RegisterIssue(IssueDetails issue)
        {
            if (issue.IssueType == "warning") RegisterWarning();
            if (issue.IssueType == "error") RegisterError();
        }

        private void RegisterWarning()
        {
            _numWarnings++;

            if (_status == DevOpsTaskResultStatus.Succeeded)
                _status = DevOpsTaskResultStatus.SucceededWithIssues;
        }

        private void RegisterError()
        {
            _numErrors++;
            _status = DevOpsTaskResultStatus.Failed;
        }

        public override string ToString()
        {
            return
                $"##vso[task.complete result={_status};]There were {_numWarnings} warnings and {_numErrors} errors encountered during compilation.";
        }
    }
}