namespace Taurit.Libraries.AzureDevOpsWarningEmitter
{
    /// <summary>
    ///     Details of a single "dotnet build" issue (like warning or error)
    /// </summary>
    public class IssueDetails
    {
        public IssueDetails(string issueType, string sourcePath, string lineNumber, string columnNumber,
            string issueCode, string message)
        {
            IssueType = issueType;
            SourcePath = sourcePath;
            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
            IssueCode = issueCode;
            Message = message;
        }

        public string IssueType { get; }
        private string SourcePath { get; }
        private string LineNumber { get; }
        private string ColumnNumber { get; }
        private string IssueCode { get; }
        private string Message { get; }

        public override string ToString()
        {
            return
                $"##vso[task.logissue type={IssueType};sourcepath={SourcePath};linenumber={LineNumber};columnnumber={ColumnNumber};code={IssueCode};]{Message}";
        }
    }
}