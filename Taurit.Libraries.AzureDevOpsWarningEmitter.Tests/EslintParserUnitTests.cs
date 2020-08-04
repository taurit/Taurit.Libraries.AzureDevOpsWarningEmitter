using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Tests
{
    [TestClass]
    public class EslintParserUnitTests
    {
        [TestMethod]
        public void When_EslintOutputWithWarningsIsGivenAsInput_Expect_AllIssuesRecognized()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.ESLint);

            // Act
            var issues = parser.GetIssues("sample-eslint-output.txt");

            // Assert
            Assert.AreEqual(544, issues.Count);
        }

        [TestMethod]
        public void When_EslintOutputWithWarningsIsGivenAsInput_Expect_WarningsAndErrorsAreProperlyDetected()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.ESLint);

            // Act
            var issues = parser.GetIssues("sample-eslint-output.txt");

            // Assert
            Assert.AreEqual(77, issues.Count(x => x.IssueType == "warning"));
            Assert.AreEqual(467, issues.Count(x => x.IssueType == "error"));
        }

        [TestMethod]
        public void When_ReactBuildOutputIsGivenAsInput_Expect_NoWarningsFound()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.ESLint);

            // Act
            var issues = parser.GetIssues("sample-react-build-output.txt");

            // Assert
            Assert.IsFalse(issues.Any());
        }

        [TestMethod]
        public void When_WarningsAreFound_Expect_AllOfRequiredFieldsAreSet()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.ESLint);

            // Act
            var issues = parser.GetIssues("sample-eslint-output.txt");

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
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.ESLint);

            // Act
            var issues = parser.GetIssues("sample-eslint-output.txt");

            // Assert
            Assert.AreEqual(issues.ToHashSet().Count, issues.Count);
        }

    }
}
