using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Tests
{
    [TestClass]
    public class DotnetParserUnitTests
    {
        [TestMethod]
        public void When_DotnetCoreBuildOutputWithWarningsIsGivenAsInput_Expect_AtLeastOneWarningFound()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.Dotnet);

            // Act
            var issues = parser.GetIssues("sample-dotnet-build-output.txt");

            // Assert
            Assert.AreEqual(26, issues.Count);
        }

        [TestMethod]
        public void When_ReactBuildOutputIsGivenAsInput_Expect_NoWarningsFound()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.Dotnet);

            // Act
            var issues = parser.GetIssues("sample-react-build-output.txt");

            // Assert
            Assert.IsFalse(issues.Any());
        }

        [TestMethod]
        public void When_WarningsAreFound_Expect_AllOfRequiredFieldsAreSet()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.Dotnet);

            // Act
            var issues = parser.GetIssues("sample-dotnet-build-output.txt");

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

        [TestMethod]
        public void When_ThereAreNoDuplicatesInInput_Expect_NoDuplicatesInOutput()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.Dotnet);

            // Act
            var issues = parser.GetIssues("sample-dotnet-build-output.txt");

            // Assert
            Assert.AreEqual(issues.ToHashSet().Count, issues.Count);
        }

        /// <summary>
        /// Related to a regex problem that occured when slashes were present in the build log and not backslashes
        /// </summary>
        [TestMethod]
        public void When_SourcePathContainsSlashes_Expect_CompleteFilenameWithPathIsPreserved()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.Dotnet);

            // Act
            var issues = parser.GetIssues("sample-dotnet-build-output-slashes.txt");

            // Assert
            Assert.AreNotEqual(0, issues.Where(x => x.SourcePath == "Service\\ServiceBusTopicClient.cs"));

            foreach (var issue in issues)
            {
                StringAssert.EndsWith(issue.SourcePath, ".cs");
            }
        }
    }
}
