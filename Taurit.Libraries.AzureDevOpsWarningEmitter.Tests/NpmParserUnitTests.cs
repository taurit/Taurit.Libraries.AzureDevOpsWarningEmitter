using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Tests
{
    [TestClass]
    public class NpmParserUnitTests
    {
        [TestMethod]
        public void When_DotnetCoreBuildOutputWithWarningsIsGivenAsInput_Expect_NoWarningsFound()
        {
            // Arrange
            var parser = BuildLogParserFactory.GetParser(BuildLogParserType.Npm);

            // Act
            var issues = parser.GetIssues("sample-dotnet-build-output.txt");

            // Assert
            Assert.AreEqual(0, issues.Count);
        }
    }
}