using System.Diagnostics.CodeAnalysis;

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

        /// <summary>
        /// "warning" or "error"
        /// </summary>
        public string IssueType { get; }
        public string SourcePath { get; }
        public string LineNumber { get; }
        public string ColumnNumber { get; }
        public string IssueCode { get; }
        public string Message { get; }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override string ToString()
        {
            return
                $"##vso[task.logissue type={IssueType};sourcepath={SourcePath};linenumber={LineNumber};columnnumber={ColumnNumber};code={IssueCode};]{Message}";
        }
    }
}