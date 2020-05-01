using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Tests
{
    [TestClass]
    public class ReactBuildParserUnitTests
    {
        [TestMethod]
        public void When_DotnetCoreBuildOutputWithWarningsIsGivenAsInput_Expect_NoWarningsFound()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.React);

            // Act
            var issues = parser.GetIssues("sample-dotnet-build-output.txt");

            // Assert
            Assert.AreEqual(0, issues.Count);
        }

        [TestMethod]
        public void When_NpmBuildOutputWithWarningsIsGivenAsInput_Expect_AllWarningsAreFound()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.React);

            // Act
            var issues = parser.GetIssues("sample-react-build-output.txt");

            // Assert
            Assert.AreEqual(3, issues.Count);
        }
        
        [TestMethod]
        public void When_WarningsAreFound_Expect_AllAvailableDataIsExposed()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.React);

            // Act
            var issues = parser.GetIssues("sample-react-build-output.txt");

            // Assert
            foreach (var issue in issues)
            {
                Assert.IsNotNull(issue);
                Assert.IsNotNull(issue.IssueType);
                Assert.IsNotNull(issue.LineNumber);
                Assert.IsNotNull(issue.ColumnNumber);
                Assert.IsNotNull(issue.IssueCode);
                Assert.IsNotNull(issue.Message);
                Assert.IsNotNull(issue.SourcePath);

                Assert.AreNotEqual(String.Empty, issue.IssueType);
                Assert.AreNotEqual(String.Empty, issue.LineNumber);
                Assert.AreNotEqual(String.Empty, issue.ColumnNumber);
                Assert.AreNotEqual(String.Empty, issue.IssueCode);
                Assert.AreNotEqual(String.Empty, issue.Message);
                Assert.AreNotEqual(String.Empty, issue.SourcePath);
            }
        }
    }
}