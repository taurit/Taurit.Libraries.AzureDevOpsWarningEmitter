namespace Taurit.Libraries.AzureDevOpsWarningEmitter.Emitting
{
    /// <summary>
    ///     Three values are allowed, as in the documentation: Succeeded, SucceededWithIssues, Failed
    ///     https://docs.microsoft.com/en-us/azure/devops/pipelines/scripts/logging-commands?view=azure-devops
    /// </summary>
    internal enum DevOpsTaskResultStatus
    {
        Succeeded,
        SucceededWithIssues,
        Failed
    }
}